using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkHWLib
{
    public class Wd1770
    {
        public static readonly byte STATUS_S0_BUSY = 0x01;
        public static readonly byte STATUS_S1_IP_DRQ = 0x02;
        public static readonly byte STATUS_S2_T0_LD = 0x04;
        public static readonly byte STATUS_S3_CRC = 0x08;
        public static readonly byte STATUS_S4_NOTFOUND = 0x10;
        public static readonly byte STATUS_S5_SURT = 0x20;
        public static readonly byte STATUS_S6_WP = 0x40;
        public static readonly byte STATUS_S7_MO = 0x80;

        public static readonly byte COMMAND_C3_H = 0x08;

        /// <summary>
        /// Top nyble encodes command type 0=idle,I..IV, 
        /// </summary>
        public enum wd1770CmdState
        {
            Idle    = 0,
            Type_I_Init,
            Type_I_SpinUp,
            Type_I_SpunUpDone,
            Type_I_StepLoop,
            Type_I_DoStep,
            Type_I_DoStep_UpdateTR,
            Type_I_DoStep_SkipUpdateTR,
            Type_I_StepOn,
            Type_I_StepOff,
            Type_I_AfterLoop,
            Type_II_Init,
            Type_III_Init,
            Type_IV_Init
        }

        public wd1770CmdState StateMachineState { get; private set; }

        /// <summary>
        /// when set (suring/after a type I command) the status flags will constantly update the WPRT/IP/TO bits
        /// </summary>
        bool followStatus;

        /// <summary>
        /// Counts index pulses until 0 before turning off / after turning on the motor.
        /// For spin up a positive integer, for spin down a negative > -100
        /// For spun up and ready <=-100
        /// </summary>
        private int SpinUpDownCounter;

        /// <summary>
        /// Current command as set by writing register 0
        /// </summary>
        public byte Command { get; private set; }
        /// <summary>
        /// Track register read/write register 1
        /// </summary>
        public byte Track { get; private set; }
        /// <summary>
        /// Sector register read/write register 2
        /// </summary>
        public byte Sector { get; private set; }
        /// <summary>
        /// Data register read/write register 3
        /// </summary>
        public byte Data { get; private set; }
        /// <summary>
        /// Status register (Read register 0)
        /// </summary>
        public byte Status { get; private set; }

        /// <summary>
        /// input:Master reset when true
        /// </summary>
        private bool _mr;
        public bool MR { 
            get => _mr;
            set
            {
                _mr = value;
                if (_mr) 
                    IntReset();
            }
        }

        /// <summary>
        /// input:Double density when true
        /// </summary>
        public bool DDEN { get; set; }

        private bool _wprt;
        /// <summary>
        /// input:write protected disk active
        /// </summary>
        public bool WRPRT {
            get => _wprt;
            set {
                if (value != _wprt)
                {
                    _wprt = value;
                    if (followStatus)
                        SetStatusBit(STATUS_S6_WP, value);
                }
            }
        }


        bool _ip;
        /// <summary>
        /// input:Index pulse from active disk
        /// </summary>
        public bool IP
        {
            get => _ip;
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    if (followStatus)
                        SetStatusBit(STATUS_S1_IP_DRQ, value);
                    if (value)
                    {
                        if (SpinUpDownCounter < 0 && SpinUpDownCounter > -100)
                        {
                            SpinUpDownCounter++;
                            if (SpinUpDownCounter == 0)
                                MO = false;
                        }
                        else if (SpinUpDownCounter > 0)
                        {
                            SpinUpDownCounter--;
                            if (SpinUpDownCounter == 0)
                                UpdateStateMachine();
                        }
                    }
                }
            }
        }


        bool _tr00;
        /// <summary>
        /// input:Track 0 on active disk
        /// </summary>
        public bool TR00
        {
            get => _tr00;
            set
            {
                if (_tr00 != value)
                {
                    _tr00 = value;
                    if (followStatus)
                        SetStatusBit(STATUS_S2_T0_LD, value);
                    if (value)
                    {
                        UpdateStateMachine();
                    }
                }
            }
        }

        bool _mo;
        /// <summary>
        /// output:Motor On output
        /// </summary>
        public bool MO { 
            get => _mo;
            private set
            {
                if (value != _mo)
                {
                    _mo = value;
                    SetStatusBit(STATUS_S7_MO, value);
                    MO_Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// output:Step direction
        /// </summary>
        public bool DIRC { get; private set; }

        public byte DSR { get; private set; }

        bool _step;
        /// <summary>
        /// output:Step head
        /// </summary>
        public bool STEP {
            get => _step;
            private set
            {
                if (_step != value)
                {
                    _step = value;
                    STEP_Changed?.Invoke(this, EventArgs.Empty);
                }
            } 
        }

        private bool _drq;
        /// <summary>
        /// output:Data request interrupt 
        /// </summary>
        public bool DRQ {
            get => _drq;
            private set {
                if (_drq != value)
                {
                    _drq = value;
                    DRQ_Changed?.Invoke(this, EventArgs.Empty);
                }
            } 
        }

        private bool _intrq;
        /// <summary>
        /// output:Command finished interrupt 
        /// </summary>
        public bool INTRQ
        {
            get => _intrq;
            private set
            {
                if (_intrq != value)
                {
                    _intrq = value;
                    INTRQ_Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected int DelayCounter { get; set; }

        public event EventHandler MO_Changed;
        public event EventHandler STEP_Changed;
        public event EventHandler DRQ_Changed;
        public event EventHandler INTRQ_Changed;

        public void Reset(bool hard)
        {
            if (hard)
            {
                IntReset();
            }
        }

        protected void IntReset() { 
            MO = false;
            DRQ = false;
            INTRQ = false;
            Command = 0;
            Status = 0;
        }

        public void Tick()
        {
            if (DelayCounter > 0)
            {
                DelayCounter--;
                if (DelayCounter == 0)
                    UpdateStateMachine();
            }
        }

        public bool Read(ushort addr, out byte dat, bool peek = false)
        {
            switch(addr & 3)
            {
                default:
                    dat = Status;
                    break;
                case 1:
                    dat = Track;
                    break;
                case 2:
                    dat = Sector;
                    break;
                case 3:
                    dat = Data;
                    break;
            }
            return true;
        }

        public bool Write(ushort addr, byte dat)
        {
            switch (addr & 3)
            {
                default:
                    UpdateCommand(dat);
                    break;
                case 1:
                    Track = dat;
                    break;
                case 2:
                    Sector = dat;
                    break;
                case 3:
                    Data = dat;
                    break;
            }
            return true;
        }

        public void UpdateCommand(byte command)
        {
            //If busy do nothing unless command is a force interrupt
            if ((Status & STATUS_S0_BUSY) != 0 && (command & 0xD0) != 0xD0)
                return; 
            Command = command;
            SetStatusBit(STATUS_S0_BUSY, true);
            switch (command & 0xE0)
            {
                default:               
                    StateMachineState = wd1770CmdState.Type_I_Init;
                    break;
                case 0x80:
                case 0xA0:
                    StateMachineState = wd1770CmdState.Type_II_Init;
                    break;
                case 0x0C:
                case 0x0E:
                    StateMachineState = wd1770CmdState.Type_III_Init;
                    break;
                case 0x0D:
                    StateMachineState = wd1770CmdState.Type_IV_Init;
                    break;
            }
            UpdateStateMachine();
        }

        public void UpdateStateMachine()
        {

        again:
#if DEBUG
            Console.WriteLine($"STATE:{StateMachineState} Status={Status}, Track={Track}, DSR={DSR}, Data={Data}");
#endif

            switch (StateMachineState)
            {
                case wd1770CmdState.Type_I_Init:
                    INTRQ = false;
                    DRQ = false;
                    SetStatusBit(STATUS_S4_NOTFOUND, false);
                    if ((Command & COMMAND_C3_H) == 0)
                    {
                        MO = true;
                        if (SpinUpDownCounter < 0)
                        {
                            //already spun up
                            SpinUpDownCounter = -100;
                        } else
                        {
                            SpinUpDownCounter = 6;
                            StateMachineState = wd1770CmdState.Type_I_SpinUp;
                            return;
                        }
                    }
                    StateMachineState = wd1770CmdState.Type_I_SpunUpDone;
                    goto again;
                case wd1770CmdState.Type_I_SpinUp:
                    if (SpinUpDownCounter == 0) {
                        StateMachineState = wd1770CmdState.Type_I_SpunUpDone;
                        goto again;
                    }
                    break;
                case wd1770CmdState.Type_I_SpunUpDone:
                    switch (Command & 0x60)
                    {
                        default:
                        case 0x00:
                            //seek/restore
                            if ((Command & 0x10) == 0)
                            {
                                //restore
                                Track = 0xFF;
                                DSR = 0x00;
                            }
                            StateMachineState = wd1770CmdState.Type_I_StepLoop;
                            goto again;
                        case 0x20:
                            StateMachineState = wd1770CmdState.Type_I_DoStep;
                            goto again;
                        case 0x40:
                            DIRC = true;
                            StateMachineState = wd1770CmdState.Type_I_DoStep;
                            goto again;
                        case 0x60:
                            DIRC = false;
                            StateMachineState = wd1770CmdState.Type_I_DoStep;
                            goto again;
                    }
                case wd1770CmdState.Type_I_StepLoop:
                    DSR = Data;
                    if (Track == DSR)
                    {
                        StateMachineState = wd1770CmdState.Type_I_AfterLoop;
                        goto again;
                    }
                    DIRC = DSR > Track;
                    StateMachineState = wd1770CmdState.Type_I_DoStep_UpdateTR;
                    goto again;
                case wd1770CmdState.Type_I_DoStep:
                    if ((Command & 0x10) == 0)
                        StateMachineState = wd1770CmdState.Type_I_DoStep_SkipUpdateTR;
                    else
                        StateMachineState = wd1770CmdState.Type_I_DoStep_UpdateTR;
                    goto again;
                case wd1770CmdState.Type_I_DoStep_UpdateTR:
                    Track += (byte)(DIRC ? 1 : -1);
                    StateMachineState = wd1770CmdState.Type_I_DoStep_SkipUpdateTR;
                    goto again;
                case wd1770CmdState.Type_I_DoStep_SkipUpdateTR:
                    if (TR00 && !DIRC)
                    {
                        Track = 0;
                        StateMachineState = wd1770CmdState.Type_I_AfterLoop;
                        goto again;
                    }
                    STEP = true;
                    DelayCounter = (DDEN) ? 2 : 4;
                    StateMachineState = wd1770CmdState.Type_I_StepOn;
                    break;
                case wd1770CmdState.Type_I_StepOn:
                    if (DelayCounter == 0)
                    {
                        STEP = false;
                        switch (Command & 0x03)
                        {
                            default:
                            case 0:
                                DelayCounter = 12000; // 6ms
                                break;
                            case 1:
                                DelayCounter = 24000; // 12ms
                                break;
                            case 2:
                                DelayCounter = 40000; // 20ms
                                break;
                            case 4:
                                DelayCounter = 60000; // 30ms
                                break;
                        }
                        StateMachineState = wd1770CmdState.Type_I_StepOff;
                    }
                    break;
                case wd1770CmdState.Type_I_StepOff:
                    if (DelayCounter == 0)
                    {
                        if ((Command & 0x60) == 0)
                            StateMachineState = wd1770CmdState.Type_I_StepLoop;
                        else
                            StateMachineState = wd1770CmdState.Type_I_AfterLoop;
                        goto again;
                    }
                    break;
            }
        }

        public void SetStatusBit(byte mask, bool value)
        {
            byte x = (byte)(Status & (mask ^ 0xFF));
            if (value)
                x |= mask;
            if (x != Status)
                Status = x;
        }
    }
}
