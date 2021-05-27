

namespace cpulib_65xx
{

	
	

	public abstract class m65x_device : cpu_device
	{

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

	}

}