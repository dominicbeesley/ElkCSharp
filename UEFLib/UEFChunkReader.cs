using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace UEFLib
{
    /// <summary>
    /// Open a possibly gzip'd uef and iterate through chunks
    /// </summary>
    public class UEFChunkReader : IDisposable
    {

        //note data doesn't contain the 12 byte header marker
        MemoryStream _data;
        private bool disposedValue;

        public bool InChunk { get; private set; }
        public int LeftInChunk { get; private set; }
        public ushort ChunkID { get; private set; }
        public int ChunkIndex { get; private set; }
        public ushort UEFVersion { get; private init; }

        public bool Wrap { get; private init; }

        public UEFChunkReader(string filename, bool wrap)
        {
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] magic = new byte[12];
                if (stream.Read(magic, 0, 2) != 2)
                {
                    stream.Dispose();
                    throw new ArgumentException("Not a UEF file");
                }

                if (magic[0] == 0x1f && magic[1] == 0x8b)
                {
                    stream.Position = 0;
                    //it's gzipped, reopen the stream as a gzip
                    stream = new GZipStream(stream, CompressionMode.Decompress);
                    stream.Read(magic, 0, 12);
                }
                else
                {
                    stream.Read(magic, 2, 10);
                }

                string hdr = new UTF8Encoding().GetString(new ReadOnlySpan<byte>(magic, 0, 10));
                //check header
                if (hdr != "UEF File!\0")
                    throw new ArgumentException("Not a UEF file");

                UEFVersion = BitConverter.ToUInt16(new ReadOnlySpan<byte>(magic, 10, 2));

                _data = new MemoryStream();
                stream.CopyTo(_data);

                Rewind();


            } finally
            {
                if (stream != null)
                    stream.Dispose();
            }            
        }

        public void Rewind()
        {
            _data.Position = 0;

            InChunk = false;
            ChunkIndex = -1;
        }

        public bool NextChunk()
        {
            if (InChunk && LeftInChunk > 0)
            {
                _data.Seek(LeftInChunk, SeekOrigin.Current);
            }

            byte[] header = new byte[6];
            int l = _data.Read(header, 0, 6);
            if (l == 0 && Wrap)
            {
                Rewind();
                l = _data.Read(header, 0, 6);
            }
            else if (l == 0)
                return false;
            if (l != 6)
                throw new Exception($"Unexpectedly short chunk header in UEF file - expected 6 bytes got {l}");

            InChunk = true;
            ChunkID = BitConverter.ToUInt16(header, 0);
            LeftInChunk = BitConverter.ToInt32(header, 2);
            ChunkIndex++;
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_data != null)
                    {
                        _data.Dispose();
                        _data = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UEFChunker()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// read upto len bytes into array from current chunk
        /// </summary>
        /// <param name="buf">the buffer to read to</param>
        /// <param name="start">start index to read to in buffer</param>
        /// <param name="len">maximum number of bytes to read</param>
        /// <returns>number of bytes read or 0 for end of chunk, -1 for no current chunk</returns>
        public int Read(byte [] buf, int start, int len)
        {
            if (!InChunk)
                return -1;
            if (len > LeftInChunk)
                len = LeftInChunk;

            int n = _data.Read(buf, start, len);
            LeftInChunk = LeftInChunk - n;
            return n;
        }
    }
}
