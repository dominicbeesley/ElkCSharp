namespace cpulib_65xx {



	public delegate void StatFn();

	public partial class m6502_device : m65x_device 
	{


		public m6502_device() :
			base()

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
		protected StatFn NextFn { get; set; }

		/// <summary>
		/// what will happen after prefetch
		/// </summary>
		protected StatFn PrefetchNextFn { get; set; }

		/// <summary>
		/// program counter
		/// </summary>
		public ushort PC { get; set; }
		
		/// <summary>
		/// stack pointer (always 100 - 1FF)
		/// </summary>
		public ushort SP { get; set; }
		
		/// <summary>
		/// temporary internal values
		/// </summary>
		protected ushort TMP { get; set; }

		/// <summary>
		/// another temporary internal value, 8 bits this time
		/// </summary>
		protected byte TMP2 { get; set; }

		/// <summary>
		/// Accumulator
		/// </summary>
		public byte A { get; set; }

		/// <summary>
		/// X index register
		/// </summary>
		public byte X { get; set; }

		/// <summary>
		/// Y index register
		/// </summary>
		public byte Y { get; set; }

		/// <summary>
		/// Processor status
		/// </summary>
		public byte P { get; set; }

		/// <summary>
		/// Prefetched instruction register
		/// </summary>
		protected byte IR { get; set; }

		/// <summary>
		/// special var for forced jmp
		/// </summary>
		protected ushort forceJMPaddr { get; set; }


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
			NextFn = m6502_device_reset_0;

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


		public override bool tick() {
			//TODO: halt needs to _not_ halt on writes for NMOS
			if (!halt_state)
			{
				NextFn?.Invoke();
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
			byte c = ((P & F_C) != 0) ? (byte)1 : (byte)0;
			P &= (F_N | F_V | F_Z | F_C)^0xFF;
			byte al = (byte)((A & 15) + (val & 15) + c);
			if (al > 9)
				al += 6;
			byte ah = (byte)((A >> 4) + (val >> 4) + ((al > 15)?1:0));
			if ((byte)(A + val + c) == 0)
				P |= F_Z;
			else if ((ah & 8) != 0)
				P |= F_N;
			if ((~(A ^ val) & (A ^ (ah << 4)) & 0x80) != 0)
				P |= F_V;
			if (ah > 9)
				ah += 6;
			if (ah > 15)
				P |= F_C;
			A = (byte)((ah << 4) | (al & 15));
		}

		void do_adc_nd(byte val)
		{
			ushort sum;
			sum = (ushort)(A + val + (((P & F_C) != 0) ? 1 : 0));
			P &= (F_N | F_V | F_Z | F_C)^0xFF;
			if ((byte)sum == 0)
				P |= F_Z;
			else if ((sum & 0x80) != 0)
				P |= F_N;
			if ((~(A ^ val) & (A ^ sum) & 0x80) != 0)
				P |= F_V;
			if ((sum & 0xff00) != 0)
				P |= F_C;
			A = (byte)sum;
		}

		void do_adc(byte val)
		{
			if ((P & F_D) != 0)
				do_adc_d(val);
			else
				do_adc_nd(val);
		}

		void do_arr_nd()
		{
			bool c = (P & F_C) != 0;
			P &= (F_N | F_Z | F_C | F_V) ^ 0xFF;
			A >>= 1;
			if (c)
				A |= 0x80;
			if (A == 0)
				P |= F_Z;
			else if ((A & 0x80) != 0)
				P |= F_N;
			if ((A & 0x40) != 0)
				P |= F_V | F_C;
			if ((A & 0x20) != 0)
				P ^= F_V;
		}

		void do_arr_d()
		{
			// The adc/ror interaction gives an extremely weird result
			bool c = (P & F_C) != 0;
			P &= (F_N | F_Z | F_C | F_V) ^ 0xFF;
			byte a = (byte)(A >> 1);
			if (c)
				a |= 0x80;
			if (a == 0)
				P |= F_Z;
			else if ((a & 0x80) != 0)
				P |= F_N;
			if (((a ^ A) & 0x40) != 0)
				P |= F_V;

			if ((A & 0x0f) >= 0x05)
				a = (byte)(((a + 6) & 0x0f) | (a & 0xf0));

			if ((A & 0xf0) >= 0x50) {
				a += 0x60;
				P |= F_C;
			}
			A = a;
		}

		void do_arr()
		{
			if ((P & F_D) != 0)
				do_arr_d();
			else
				do_arr_nd();
		}

		void do_cmp(byte val1, byte val2)
		{
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			ushort r = (ushort)(val1 - val2);
			if (r == 0)
				P |= F_Z;
			else if ((r & 0x80) !=0 )
				P |= F_N;
			if ((r & 0xff00) == 0)
				P |= F_C;
		}

		void do_sbc_d(byte val)
		{
			byte c = ((P & F_C)!=0) ? (byte)0 : (byte)1;
			P &= (F_N | F_V | F_Z | F_C) ^ 0xFF;
			ushort diff = (ushort)(A - val - c);
			byte al = (byte)((A & 15) - (val & 15) - c);
			if ((al & 0x80) != 0)
				al -= 6;
			byte ah = (byte)((A >> 4) - (val >> 4) - ((al & 0x80)!=0?(byte)1:(byte)0));
			if (((byte)diff) != 0)
				P |= F_Z;
			else if ((diff & 0x80) != 0)
				P |= F_N;
			if (((A ^ val) & (A ^ diff) & 0x80) != 0)
				P |= F_V;
			if ((diff & 0xff00) != 0)
				P |= F_C;
			if ((ah & 0x80) != 0)
				ah -= 6;
			A = (byte)((ah << 4) | (al & 15));
		}

		void do_sbc_nd(byte val)
		{
			ushort diff = (ushort)(A - val - ((P & F_C)!=0 ? 0 : 1));
			P &= (F_N | F_V | F_Z | F_C) ^ 0xFF;
			if ((byte)diff != 0)
				P |= F_Z;
			else if ((diff & 0x80) != 0)
				P |= F_N;
			if (((A ^ val) & (A ^ diff) & 0x80) != 0)
				P |= F_V;
			if ((diff & 0xff00) == 0)
				P |= F_C;
			A = (byte)diff;
		}

		void do_sbc(byte val)
		{
			if ((P & F_D) != 0)
				do_sbc_d(val);
			else
				do_sbc_nd(val);
		}

		void do_bit(byte val)
		{
			P &= (F_N | F_Z | F_V) ^ 0xFF;
			byte r = (byte)(A & val);
			if (r == 0)
				P |= F_Z;
			if ((val & 0x80) != 0)
				P |= F_N;
			if ((val & 0x40) != 0)
				P |= F_V;
		}

		byte do_asl(byte v)
		{
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			byte r = (byte)(v << 1);
			if (r == 0)
				P |= F_Z;
			else if ((r & 0x80) != 0)
				P |= F_N;
			if ((v & 0x80) != 0)
				P |= F_C;
			return r;
		}

		byte do_lsr(byte v)
		{
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				P |= F_C;
			v >>= 1;
			if (v != 0)
				P |= F_Z;
			return v;
		}

		byte do_ror(byte v)
		{
			bool c = (P & F_C) != 0;
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				P |= F_C;
			v >>= 1;
			if (c)
				v |= 0x80;
			if (v == 0)
				P |= F_Z;
			else if ((v & 0x80) != 0)
				P |= F_N;
			return v;
		}

		byte do_rol(byte v)
		{
			bool c = (P & F_C) != 0;
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 0x80) != 0)
				P |= F_C;
			v <<= 1;
			if (c)
				v |= 0x01;
			if (v == 0)
				P |= F_Z;
			else if ((v & 0x80) != 0)
				P |= F_N;
			return v;
		}

		byte do_asr(byte v)
		{
			P &= (F_N | F_Z | F_C) ^ 0xFF;
			if ((v & 1) != 0)
				P |= F_C;
			v >>= 1;
			if (v == 0)
				P |= F_Z;
			else if ((v & 0x40) != 0) {
				P |= F_N;
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
					P |= F_V;
				v_state = state == cpu_65xx_inputstate.ASSERT_LINE;
				break;
			case cpu_65xx_inputlines.INPUT_LINE_HALT:
				halt_state = state == cpu_65xx_inputstate.ASSERT_LINE;
				break;
			}
		}

		protected void set_nz(byte v)
		{
			P &= (F_Z | F_N) ^ 0xFF;
			if ((v & 0x80) != 0)
				P |= F_N;
			if (v == 0)
				P |= F_Z;
		}



		/* shared state functions */

		protected void m6502_device_postfetch()
		{
			IR = DAT;
			sync = false;
			//sync_w(CLEAR_LINE);

			if (!skip_ints_next && (nmi_state || (irq_state && !((P & F_I)!=0))) && !inhibit_interrupts) {
				irq_taken = true;
				IR = 0x00;
			}
			else
				PC++;
			skip_ints_next = false;

			postfetch_int();
		}

		protected void m6502_device_prefetch()
		{
			sync = true;
			//sync_w(ASSERT_LINE);
			ADDR = PC;
			RNW = true;
			NextFn = PrefetchNextFn;
		}

		//TODO: speed up (don't mess around with PrefetchNextFn here?

		protected void m6502_device_fetch() {
			PrefetchNextFn = m6502_device_postfetch;
			m6502_device_prefetch();
		}

		protected void m6502_device_fetch_noirq() {
			skip_ints_next = true;
			PrefetchNextFn = m6502_device_postfetch;
			m6502_device_prefetch();
		}

		protected void m6502_device_forceJMP() {
			PC = forceJMPaddr;
			m6502_device_fetch();
		}

		protected void forceJMP(ushort val)
		{
			forceJMPaddr = val;
			NextFn = m6502_device_forceJMP;
		}

	}
}