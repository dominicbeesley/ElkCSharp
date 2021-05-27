
namespace cpulib_65xx {

		
	// ======================> cpu_device

	public abstract class cpu_device 
	{
		// I/O line definitions
		public enum cpu_65xx_inputlines
		{
			// input lines
			INPUT_LINE_IRQ0,
			INPUT_LINE_NMI,
			INPUT_LINE_V,

			// special input lines that are implemented in the core
			INPUT_LINE_RESET,
			INPUT_LINE_HALT
		};

		public enum cpu_65xx_inputstate
		{
			CLEAR_LINE = 0,
			ASSERT_LINE = 1
		}



		public abstract void start();
		public abstract void reset();

		public abstract void execute_set_input(cpu_65xx_inputlines inputnum, cpu_65xx_inputstate state);

		public abstract bool tick();



	}

}