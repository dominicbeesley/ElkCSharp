using System;
using UEFLib;

namespace UEFCatalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buf = new byte[8];


            using (var uef = new UEFChunkReader(args[0], false))
            {
                while (uef.NextChunk())
                {
                    Console.WriteLine($"ID={uef.ChunkID:X2}, Length={uef.LeftInChunk:X4}");
                    int x = uef.Read(buf, 0, 8);
                    for (int i = 0; i < x; i++)
                    {
                        Console.Write($" {buf[i]:X2}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
