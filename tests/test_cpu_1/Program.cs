﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using cpulib_65xx;

namespace test_cpu_1
{
    class Program : ISYSCpu
    {


        byte[] store = new byte[0x10000];

        m6502_device cpu;

        public bool Read(ushort addr, ref byte dat, bool peek=false)
        {
            dat = store[addr];
			return true;
        }

		public void Write(ushort addr, byte dat)
        {
            store[addr] = dat;
        }



        const long MAXCYCLES = 1000000000L;

        static void Usage(TextWriter o, string msg, Exception ex = null) {
	        if (msg != null)
                o.WriteLine(msg);

			if (ex!= null)
            {
				o.WriteLine(ex.ToString());
            }

            o.WriteLine(
@$"cpu_test1 <rom image>

Expects a rom image 16k based at 0xC000, runs for { MAXCYCLES } cycles 
and returns the flat-out clock speed

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

		protected int Run(string mosfilename) { 

			FileStream brMos;

			try
            {
				brMos = new FileStream(mosfilename, FileMode.Open, FileAccess.Read);
            } catch (Exception ex)
            {
				Usage(Console.Error, $"Cannot open {mosfilename} for input", ex);
				return 3;
            }
			using (brMos)
            {
				brMos.Read(store, 0xC000, 0x4000);
            }

			cpu = new m6502_device(this);

			cpu.start();
			cpu.reset();

			Console.Out.WriteLine($"Running for {MAXCYCLES} cycles...");

			Stopwatch sw = new Stopwatch();
			sw.Start();

			for (long i = 0; i < MAXCYCLES; i++)
			{
				cpu.tick();
				//Console.WriteLine($"PC={cpu.PC:X4},A={cpu.A:X2},X={cpu.X:X2},Y={cpu.Y:X2},P={cpu.P:X2},ADDR={cpu.ADDR:X4},DAT={cpu.DAT:X2}");

				//cout << cpu;
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
