using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using cpulib_65xx;

namespace test_cpu_dorman
{
	class Program : ISYSCpu
	{

		bool verby = false;
		byte[] store = new byte[0x10000];

		m6502_device cpu;

		public bool Read(ushort addr, out byte dat, bool peek=false)
		{
			if (addr == 0xF004)
			{
				if (Console.KeyAvailable)
				{
					var k = Console.ReadKey(true);
					dat = Encoding.Default.GetBytes(new char[] { k.KeyChar })[0];
				} else
                {
					dat = 0;
                }
			}
			else
			{
				dat = store[addr];
			}

			if (verby & !peek)
				Console.WriteLine($"  ${addr:X4} ==> ${dat:X2}");

			return true;
		}

		public bool Write(ushort addr, byte dat)
		{
			if (addr == 0xF001)
			{
				Console.Write(Encoding.Default.GetString(new byte[] { dat }));
			}
			else
			{
				store[addr] = dat;
			}

			if (verby)
				Console.WriteLine($"  ${addr:X4} <== ${dat:X2}");

			return true;
		}



		const long MAXCYCLES = 100000000L;

		static void Usage(TextWriter o, string msg, Exception ex = null)
		{
			if (msg != null)
				o.WriteLine(msg);

			if (ex != null)
			{
				o.WriteLine(ex.ToString());
			}

			o.WriteLine(
@$"test_cpu_dorman <rom image>

Expects a full 64k ram image. Read/Write char hardware at F004/F001

"
				);

		}


		static int Main(string[] args)
		{

			if (args.Length != 1)
			{
				Usage(Console.Error, "Too few arguments");

				return 2;
			}


			var p = new Program();
			return p.Run(args[0]);

		}

		protected int Run(string mosfilename)
		{

			FileStream brMos;

			try
			{
				brMos = new FileStream(mosfilename, FileMode.Open, FileAccess.Read);
			}
			catch (Exception ex)
			{
				Usage(Console.Error, $"Cannot open {mosfilename} for input", ex);
				return 3;
			}
			using (brMos)
			{
				if (brMos.Read(store, 0x0000, 0x10000) != 0x10000)
				{
					Console.Error.WriteLine("Warning small ram image detected");
				}
			}

			cpu = new m6502_device(this);

			cpu.start();
			cpu.reset();

			Stopwatch sw = new Stopwatch();
			sw.Start();

			for (long i = 0; i < MAXCYCLES; i++)
			{

				//if (cpu.PC == 0x2ABB)
				//	verby = true;
				if (cpu.PC == 0x4458)
					verby = false;

				cpu.tick();
				if (verby)
				{
					if (cpu.Sync)
                    {
						Console.WriteLine(cpu.Disassemble(cpu.PC));
                    }
					Console.WriteLine($"PC={cpu.PC:X4},A={cpu.A:X2},X={cpu.X:X2},Y={cpu.Y:X2},P={cpu.P:X2}({cpu.FlagsToString(cpu.P)}),ADDR={cpu.ADDR:X4},DAT={cpu.DAT:X2}");
				}

			}

			sw.Stop();

			Console.Out.Write(
$@"{MAXCYCLES} in {sw.ElapsedMilliseconds}ms
  = {(((double)MAXCYCLES / 1000.0) / (double)sw.ElapsedMilliseconds):0.###}MHz
"

			);

			Console.ReadLine();

			return 0;
		}
	}
}
