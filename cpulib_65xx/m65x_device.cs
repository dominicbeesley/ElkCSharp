

namespace cpulib_65xx
{

	

	public abstract class m65x_device : cpu_device
	{

		public m65x_device() : base()
		{
			//sync_w(*this),
			DAT = 0;
			sync = false;
			ADDR = 0;
			RNW = true;
		}

		public virtual void init()
		{

			RNW = true;
			sync = false;
			ADDR = 0;
			DAT = 0;
		}


		public ushort ADDR { get; protected set; }
		public byte DAT { get; set; }
		public bool RNW { get; protected set; }
		public bool sync { get; protected set; }

		//should really be abstract but then blitter which doesn't have the concept would have to override
		protected virtual void postfetch_int() { }

		public override void start()
		{
			init();
		}

		public override void reset()
		{

			sync = false;
			RNW = true;
		}

	}

}