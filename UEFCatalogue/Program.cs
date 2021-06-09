using System;
using UEFLib;

namespace UEFCatalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uef = new UEFChunker(args[0], false))
            {
                while (uef.NextChunk())
                {
                    Console.WriteLine($"ID={uef.ChunkID:X2}, Length={uef.LeftInChunk:X4}");
                }
            }
        }
    }
}
