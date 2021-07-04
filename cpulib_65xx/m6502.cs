using System.Runtime.CompilerServices;

namespace cpulib_65xx {



//	public delegate void StatFn();

	public unsafe partial class m6502_device : m65x_device 
	{


		public m6502_device(ISYSCpu syscpu) :
			base(syscpu)

		{
			PC = 0;
			SP = 0;
			TMP = 0;
			TMP2 = 0;
			A = 0;
			X = 0;
			Y = 0;
			P = 0;
			IR = 0;
			NextFn = null;
			nmi_state = false;
			irq_state = false;
			v_state = false;
			irq_taken = false;
			inhibit_interrupts = false;
		}


		protected const byte F_N = 0x80;
		protected const byte F_V = 0x40;
		/// <summary>
		/// 65ce02
		/// </summary>
		protected const byte F_E = 0x20; 
			/// <summary>
			///  // M740: replaces A with $00,X in some opcodes when set
			/// </summary>
		protected const byte F_T = 0x20;
		protected const byte F_B = 0x10;
		protected const byte F_D = 0x08;
		protected const byte F_I = 0x04;
		protected const byte F_Z = 0x02;
		protected const byte F_C = 0x01;



		/// <summary>
		/// what will happen in next cycle
		/// </summary>
		delegate*<m6502_device, void> NextFn;

		/// <summary>
		/// what will happen after prefetch
		/// </summary>
		delegate*<m6502_device, void> PrefetchNextFn;

		private ushort _pc;
		/// <summary>
		/// program counter
		/// </summary>
		public ushort PC { get => _pc; set => _pc = value; }

		private ushort _sp;
		/// <summary>
		/// stack pointer (always 100 - 1FF)
		/// </summary>
		/// 
		public ushort SP { get => _sp; set => _sp = value; }

		private ushort _tmp;

		/// <summary>
		/// temporary internal values
		/// </summary>
		protected ushort TMP { get => _tmp; set => _tmp = value; }


		private byte _tmp2;
		/// <summary>
		/// another temporary internal value, 8 bits this time
		/// </summary>
		protected byte TMP2 { get => _tmp2; set => _tmp2 = value; }

		private byte _a;
		/// <summary>
		/// Accumulator
		/// </summary>
		public byte A { get => _a; set => _a = value; }

		private byte _x;
		/// <summary>
		/// X index register
		/// </summary>
		public byte X { get => _x; set => _x = value; }

		private byte _y;
		/// <summary>
		/// Y index register
		/// </summary>
		public byte Y { get => _y; set => _y = value; }


		private byte _p;
		/// <summary>
		/// Processor status
		/// </summary>
		public byte P { get => _p; set => _p = value; }

		private byte _ir;
		/// <summary>
		/// Prefetched instruction register
		/// </summary>
		protected byte IR { get => _ir; set => _ir = value; }


		private ushort _forceJMPaddr;
		/// <summary>
		/// special var for forced jmp
		/// </summary>
		protected ushort forceJMPaddr { get => _forceJMPaddr; set => _forceJMPaddr = value; }


		bool	  skip_ints_next;			  /* Do not check for interrupts on this fetch*/


		bool nmi_state, irq_state, v_state, halt_state;
		bool irq_taken, inhibit_interrupts;


		//TODO: try [MethodImpl(MethodImplOptions.AggressiveInlining)] inline helpers
		bool page_changing(ushort baseaddr, int delta) { return (((baseaddr + delta) ^ baseaddr) & 0xff00) != 0; }
		ushort set_l(ushort baseaddr, byte val) { return (ushort)((baseaddr & (ushort)0xff00) | val); }
		ushort set_h(ushort baseaddr, byte val) { return (ushort)((baseaddr & 0x00ff) | (val << 8)); }

		void dec_SP() { SP = set_l(SP, (byte)(SP - 1)); }
		void inc_SP() { SP = set_l(SP, (byte)(SP + 1)); }



		public override void reset()
		{

			nmi_state = false;
			irq_state = false;
			irq_taken = false;
			v_state = false;
			inhibit_interrupts = false;
			skip_ints_next = false;

			base.reset();
			NextFn = &m6502_device_reset_0;

		}



		public override void init()
		{
			base.init();

			PC = 0x0000;
			A = 0x00;
			X = 0x80;
			Y = 0x00;
			P = 0x36;
			SP = 0x0100;
			TMP = 0x0000;
			TMP2 = 0x00;
			IR = 0x00;
			reset();
		}


		protected void _doread(ushort a)
        {
			_addr = a;
			_rnw = true;
        }

		protected void _dowrite(ushort a, byte d)
        {
			_addr = a;
			_rnw = false;
			_dat = d;
        }

		public override bool tick() {
			//TODO: halt needs to _not_ halt on writes for NMOS
			if (!halt_state)
			{

				NextFn(this);

				if (_rnw)
					SysCpu.Read(_addr, ref _dat);
				else
					SysCpu.Write(_addr, _dat);


				return true;
			}
			else {
				return false;
			}
		}

		/*
		std::ostream& operator<<(std::ostream& o, m6502_device& dev)
		{
			std::ios_base::fmtflags oldflags = o.flags();
			char oldfill = o.fill();

			o << std::hex;
			o.fill('0');

			o << "A:" << std::setw(2) << (int)dev.A;
			o << ", X:" << std::setw(2) << (int)dev.X;
			o << ", Y:" << std::setw(2) << (int)dev.Y;
			o << ", S:" << std::setw(2) << (int)dev.SP;
			o.width(4);
			o << ",PC:" << std::setw(4) << (int)dev.PC;
			o << ",ADR:" << std::setw(4) << (int)dev.ADDR;
			o << ",RnW:" << (dev.RNW) ? "R" : "W";
			o << ",D:" << std::setw(2) << (int)dev.DAT;
			o << ",IR:" << std::setw(2) << (int)dev.IR;

			o << ", P:";

			int f = dev.P;
			const char flagsTempl[] = "nvobdizc";
			const char* p = flagsTempl;
			for (int i = 0; i < 8; i++) {
				if (f & 0x80)
					o << (char)toupper(*p);
				else
					o << *p;
				*p++;
				f = f << 1;
			}

			//o << ",st:" << std::setw(2) << (int)dev.inst_state << ":" << std::setw(2) << dev.inst_substate;

			if (dev.get_sync()) {
				o << " SYNC";
			}

			o << std::endl;

			o.flags(oldflags);
			o.fill(oldfill);

			return o;
		}
		*/

		void do_adc_d(byte val)
		{
			byte c = ((_p & F_C) != 0) ? (byte)1 : (byte)0;
			_p &= (F_N | F_V | F_Z | F_C)^0xFF;
			byte al = (byte)((_a & 15) + (val & 15) + c);
			if (al > 9)
				al += 6;
			byte ah = (byte)((_a >> 4) + (val >> 4) + ((al > 15)?1:0));
			if ((byte)(_a + val + c) == 0)
				_p |= F_Z;
			else if ((ah & 8) != 0)
				_a |= F_N;
			if ((~(_a ^ val) & (_a ^ (ah << 4)) & 0x80) != 0)
				_p |= F_V;
			if (ah > 9)
				ah += 6;
			if (ah > 15)
				_p |= F_C;
			_a = (byte)((ah << 4) | (al & 15));
		}

		void do_adc_nd(byte val)
		{
			ushort sum;
			sum = (ushort)(_a + val + (((_p & F_C) != 0) ? 1 : 0));
			_p &= (F_N | F_V | F_Z | F_C)^0xFF;
			if ((byte)sum == 0)
				_p |= F_Z;
			else if ((sum & 0x80) != 0)
				_p |= F_N;
			if ((~(_a ^ val) & (_a ^ sum) & 0x80) != 0)
				_p |= F_V;
			if ((sum & 0xff00) != 0)
				_p |= F_C;
			_a = (byte)sum;
		}

		void do_adc(byte val)
		{
			if ((_p & F_D) != 0)
				do_adc_d(val);
			else
				do_adc_nd(val);
		}

		void do_arr_nd()
		{
			bool c = (_p & F_C) != 0;
			_p &= (F_N | F_Z | F_C | F_V) ^ 0xFF;
			_a >>= 1;
			if (c)
				_a |= 0x80;
			if (_a == 0)
				_p |= F_Z;
			else if ((_a & 0x80) != 0)
				_p |= F_N;
			if ((_a & 0x40) != 0)
				_p |= F_V | F_C;
			if ((_a & 0x20) != 0)
				_p ^= F_V;
		}

		void do_arr_d()
		{
			// The adc/ror interaction gives an extremely weird result
			bool c = (_p & F_C) != 0;
			_p &= (F_N | F_Z | F_C | F_V) ^ 0xFF;
			byte a = (byte)(_a >> 1);
			if (c)
				a |= 0x80;
			if (a == 0)
				_p |= F_Z;
			else if ((a & 0x80) != 0)
				_p |= F_N;
			if (((a ^ _a) & 0x40) != 0)
				_p |= F_V;

			if ((_a & 0x0f) >= 0x05)
				a = (byte)(((a + 6) & 0x0f) | (a & 0xf0));

			if ((_a & 0xf0) >= 0x50) {
				a += 0x60;
				_p |= F_C;
			}
			_a = a;
		}

		void do_arr()
		{
			if ((_p & F_D) != 0)
				do_arr_d();
			else
				do_arr_nd();
		}

		void do_cmp(byte val1, byte val2)
		{
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			ushort r = (ushort)(val1 - val2);
			if (r == 0)
				_p |= F_Z;
			else if ((r & 0x80) !=0 )
				_p |= F_N;
			if ((r & 0xff00) == 0)
				_p |= F_C;
		}

		void do_sbc_d(byte val)
		{
			byte c = ((_p & F_C)!=0) ? (byte)0 : (byte)1;
			_p &= (F_N | F_V | F_Z | F_C) ^ 0xFF;
			ushort diff = (ushort)(_a - val - c);
			byte al = (byte)((_a & 15) - (val & 15) - c);
			if ((al & 0x80) != 0)
				al -= 6;
			byte ah = (byte)((_a >> 4) - (val >> 4) - ((al & 0x80)!=0?(byte)1:(byte)0));
			if (((byte)diff) != 0)
				_p |= F_Z;
			else if ((diff & 0x80) != 0)
				_p |= F_N;
			if (((_a ^ val) & (_a ^ diff) & 0x80) != 0)
				_p |= F_V;
			if ((diff & 0xff00) == 0)
				_p |= F_C;
			if ((ah & 0x80) != 0)
				ah -= 6;
			_a = (byte)((ah << 4) | (al & 15));
		}

		void do_sbc_nd(byte val)
		{
			ushort diff = (ushort)(_a - val - ((_p & F_C)!=0 ? 0 : 1));
			_p &= (F_N | F_V | F_Z | F_C) ^ 0xFF;
			if ((byte)diff == 0)
				_p |= F_Z;
			else if ((diff & 0x80) != 0)
				_p |= F_N;
			if (((_a ^ val) & (_a ^ diff) & 0x80) != 0)
				_p |= F_V;
			if ((diff & 0xff00) == 0)
				_p |= F_C;
			_a = (byte)diff;
		}

		void do_sbc(byte val)
		{
			if ((_p & F_D) != 0)
				do_sbc_d(val);
			else
				do_sbc_nd(val);
		}

		void do_bit(byte val)
		{
			_p &= (F_N | F_Z | F_V) ^ 0xFF;
			byte r = (byte)(_a & val);
			if (r == 0)
				_p |= F_Z;
			if ((val & 0x80) != 0)
				_p |= F_N;
			if ((val & 0x40) != 0)
				_p |= F_V;
		}

		byte do_asl(byte v)
		{
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			byte r = (byte)(v << 1);
			if (r == 0)
				_p |= F_Z;
			else if ((r & 0x80) != 0)
				_p |= F_N;
			if ((v & 0x80) != 0)
				_p |= F_C;
			return r;
		}

		byte do_lsr(byte v)
		{
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				_p |= F_C;
			v >>= 1;
			if (v == 0)
				_p |= F_Z;
			return v;
		}

		byte do_ror(byte v)
		{
			bool c = (_p & F_C) != 0;
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				_p |= F_C;
			v >>= 1;
			if (c)
				v |= 0x80;
			if (v == 0)
				_p |= F_Z;
			else if ((v & 0x80) != 0)
				_p |= F_N;
			return v;
		}

		byte do_rol(byte v)
		{
			bool c = (_p & F_C) != 0;
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 0x80) != 0)
				_p |= F_C;
			v <<= 1;
			if (c)
				v |= 0x01;
			if (v == 0)
				_p |= F_Z;
			else if ((v & 0x80) != 0)
				_p |= F_N;
			return v;
		}

		byte do_asr(byte v)
		{
			_p &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				_p |= F_C;
			v >>= 1;
			if (v == 0)
				_p |= F_Z;
			else if ((v & 0x40) != 0) {
				_p |= F_N;
				v |= 0x80;
			}
			return v;
		}


		//TODO: enum for inputnum, state?
		public override void execute_set_input(cpu_65xx_inputlines inputnum, cpu_65xx_inputstate state)
		{
			switch (inputnum) {
			case cpu_65xx_inputlines.INPUT_LINE_IRQ0: irq_state = state == cpu_65xx_inputstate.ASSERT_LINE; break;
			case cpu_65xx_inputlines.INPUT_LINE_NMI: nmi_state = nmi_state || (state == cpu_65xx_inputstate.ASSERT_LINE); break;
			case cpu_65xx_inputlines.INPUT_LINE_V:
				if (!v_state && state == cpu_65xx_inputstate.ASSERT_LINE)
					_p |= F_V;
				v_state = state == cpu_65xx_inputstate.ASSERT_LINE;
				break;
			case cpu_65xx_inputlines.INPUT_LINE_HALT:
				halt_state = state == cpu_65xx_inputstate.ASSERT_LINE;
				break;
			}
		}

		protected void set_nz(byte v)
		{
			_p &= (F_Z | F_N) ^ 0xFF;
			if ((v & 0x80) != 0)
				_p |= F_N;
			if (v == 0)
				_p |= F_Z;
		}



		/* shared state functions */
		protected static void m6502_device_postfetch(m6502_device cpu)
		{
			cpu._ir = cpu._dat;
			cpu._sync = false;
			//sync_w(CLEAR_LINE);

			if (!cpu.skip_ints_next && (cpu.nmi_state || (cpu.irq_state && !((cpu._p & F_I)!=0))) && !cpu.inhibit_interrupts) {
				cpu.irq_taken = true;
				cpu._ir = 0x00;
			}
			else
				cpu._pc++;
			cpu.skip_ints_next = false;

			cpu.postfetch_int();
		}

		protected static void m6502_device_prefetch(m6502_device cpu)
		{
			cpu._sync = true;
			//sync_w(ASSERT_LINE);
			cpu._addr = cpu._pc;
			cpu._rnw = true;
			cpu.NextFn = cpu.PrefetchNextFn;
		}

		//TODO: speed up (don't mess around with PrefetchNextFn here?

		protected static void m6502_device_fetch(m6502_device cpu) {
			cpu.PrefetchNextFn = &m6502_device_postfetch;
			m6502_device_prefetch(cpu);
		}

		protected static void m6502_device_fetch_noirq(m6502_device cpu) {
			cpu.skip_ints_next = true;
			cpu.PrefetchNextFn = &m6502_device_postfetch;
			m6502_device_prefetch(cpu);
		}

		protected static void m6502_device_forceJMP(m6502_device cpu) {
			cpu._pc = cpu._forceJMPaddr;
			m6502_device_fetch(cpu);
		}

		protected void forceJMP(ushort val)
		{
			_forceJMPaddr = val;
			NextFn = &m6502_device_forceJMP;
		}

	}
}