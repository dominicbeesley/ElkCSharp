

namespace cpulib_65xx
{

	
	

	public abstract class m65x_device : cpu_device
	{

		public enum DisOperandType
		{
			Implied,
			Relative,
			Absolute,
			AbsoluteX,
			AbsoluteY,
			Immediate,
			Indirect,
			IndirectX,
			IndirectY,
			Zeropage,
			ZeropageX,
			ZeropageY,
			Accumulator
		};

		public struct DisOpDetails
        {
			public string Mnemonic { get; init; }
			public DisOperandType OperandType { get; init; }
        }

		public abstract DisOpDetails[] OpCodes { get; }

		public ISYSCpu SysCpu { get; private set; }

		public m65x_device(ISYSCpu syscpu) : base()
		{
			SysCpu = syscpu;

			//sync_w(*this),
			DAT = 0;
			Sync = false;
			ADDR = 0;
			RNW = true;
		}

		public virtual void init()
		{

			RNW = true;
			Sync = false;
			ADDR = 0;
			DAT = 0;
		}


		protected ushort _addr;
		public ushort ADDR { get => _addr; protected set => _addr = value; }

		protected byte _dat;
		public byte DAT { get => _dat; set => _dat = value; }

		protected bool _rnw;
		public bool RNW { get => _rnw; protected set => _rnw = value; }

		protected bool _sync;
		public bool Sync { get => _sync; protected set => _sync = value; }

		//should really be abstract but then blitter which doesn't have the concept would have to override
		protected virtual void postfetch_int() { }

		public override void start()
		{
			init();
		}

		public override void reset()
		{

			Sync = false;
			RNW = true;
		}

		public virtual string FlagsToString(byte flags)
        {
			char[] ret = new char [] { 'n', 'v', 'x', 'b', 'd', 'i', 'z', 'c' };
			for (int i = 0; i < 8; i++)
            {
				if ((flags & 0x80) != 0)
                {
					ret[i] = char.ToUpper(ret[i]);
                }
				flags = (byte)(flags << 1);
			}
			return new string(ret);
        }

		public string Disassemble(ushort addr)
        {
			byte op;

			SysCpu.Read(addr, out op, true);

			var opd = OpCodes[op];

			byte peek(ushort addr) {
				byte ret;
				SysCpu.Read(addr, out ret, true);
				return ret;
            };

			string operands = "";
			switch (opd.OperandType)
            {
				case DisOperandType.Immediate:
					operands = $"#{peek((ushort)(addr + 1)):X2}";
					break;
				case DisOperandType.Implied:
					operands = "";
					break;

				case DisOperandType.Relative:
					operands = $"{(addr + 2 + (sbyte)peek((ushort)(addr + 1))):X4}";
					break;
				case DisOperandType.Absolute:
					operands = $"{peek((ushort)(addr + 1)) + (peek((ushort)(addr + 2)) << 8):X4}";
					break;
				case DisOperandType.AbsoluteX:
					operands = $"{peek((ushort)(addr + 1)) + (peek((ushort)(addr + 2)) << 8):X4}, X";
					break;
				case DisOperandType.AbsoluteY:
					operands = $"{peek((ushort)(addr + 1)) + (peek((ushort)(addr + 2)) << 8):X4}, Y";
					break;
				case DisOperandType.Indirect:
					operands = $"({peek((ushort)(addr + 1)) + (peek((ushort)(addr + 2)) << 8):X4})";
					break;
				case DisOperandType.IndirectX:
					operands = $"({peek((ushort)(addr + 1)):X2}, X)";
					break;
				case DisOperandType.IndirectY:
					operands = $"({peek((ushort)(addr + 1)):X2}), Y";
					break;
				case DisOperandType.Zeropage:
					operands = $"{peek((ushort)(addr + 1)):X2}";
					break;
				case DisOperandType.ZeropageX:
					operands = $"{peek((ushort)(addr + 1)):X2}, X";
					break;
				case DisOperandType.ZeropageY:
					operands = $"{peek((ushort)(addr + 1)):X2}, Y";
					break;
				case DisOperandType.Accumulator:
					operands = "A";
					break;
				default:
					operands = "?";
					break;
            }

			return $"{addr:X4} : {opd.Mnemonic}     {operands}";

        }
	}

}