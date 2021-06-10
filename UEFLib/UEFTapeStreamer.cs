using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UEFLib
{
    
    public enum UEFTapeBit { Blank, HighTone, LowTone };

    public class UEFTapeStreamer : IDisposable
    {
        private UEFChunkReader _chunkReader;

        private UEFTapeBit _curBit;
        private int _repeat;

        private ushort _curDataByte;
        private int _bitsLeft;
        private bool disposedValue;

        public UEFTapeStreamer(string filename, bool wrap)
        {
            _chunkReader = new UEFChunkReader(filename, wrap);
            _curBit = UEFTapeBit.Blank;
            _repeat = 0;
            _bitsLeft = 0;
        }

        UEFTapeBit doNextImplicit()
        {
            //next byte with implicit start/stop bits 0x100
            byte[] d = new byte[1];
            _chunkReader.Read(d, 0, 1);
            _bitsLeft = 9;
            _curDataByte = (ushort)(d[0] | 0x100);
            return UEFTapeBit.LowTone;
        }

        UEFTapeBit doNextExplicit()
        {
            //next byte with embedded start/stop bits 0x102
            byte[] d = new byte[1];
            _chunkReader.Read(d, 0, 1);
            _bitsLeft = 7;
            _curDataByte = (ushort)(d[0]);
            UEFTapeBit ret = ((_curDataByte & 0x1) != 0) ? UEFTapeBit.HighTone : UEFTapeBit.LowTone;
            _curDataByte >>= 1;
            return ret;
        }

        void doRepeated()
        {
            byte[] d = new byte[2];
            _chunkReader.Read(d, 0, 2);
            _repeat = BitConverter.ToUInt16(d, 0);
        }

        private bool carrierDummy = false;

        //This should be called once every 1/1200th of a second and will return the next "tone" from the tape        
        public UEFTapeBit Tick() 
        {
            if (_repeat <= 0)
            {
                if (_bitsLeft > 0)
                {
                    bool x = (_curDataByte & 0x1) != 0;
                    _curDataByte >>= 1;
                    _bitsLeft--;
                    _curBit = x ? UEFTapeBit.HighTone : UEFTapeBit.LowTone;
                }
                else if (_chunkReader.ChunkID == 0x100 && _chunkReader.LeftInChunk > 0)
                {
                    _curBit = doNextImplicit();
                }
                else if (_chunkReader.ChunkID == 0x102 && _chunkReader.LeftInChunk > 0)
                {
                    _curBit = doNextExplicit();
                }
                else if (_chunkReader.ChunkID == 0x111 && _chunkReader.LeftInChunk > 0)
                {
                    if (carrierDummy)
                    {
                        carrierDummy = false;
                        _curDataByte = 0x1AA;
                        _bitsLeft = 9;
                        _curBit = UEFTapeBit.LowTone;
                    }
                    else
                    {
                        doRepeated();
                        _curBit = UEFTapeBit.HighTone;
                    }
                }
                else
                {
                    //we're either at then end of a chunk, not chunk open, or we're lost...start another chunk

                    bool recognised = false;
                    int startChunkIndex = _chunkReader.ChunkIndex;
                    while (!recognised && _chunkReader.ChunkIndex != startChunkIndex - 1)
                    {
                        _chunkReader.NextChunk();
                        switch (_chunkReader.ChunkID)
                        {
                            case 0x100:
                                _curBit = doNextImplicit();
                                recognised = true;
                                break;
                            case 0x102:
                                _curBit = doNextExplicit();
                                recognised = true;
                                break;
                            case 0x111:
                            case 0x110:
                                _curBit = UEFTapeBit.HighTone;
                                doRepeated();
                                recognised = true;
                                break;
                            case 0x112:
                                _curBit = UEFTapeBit.Blank;
                                doRepeated();
                                recognised = true;
                                break;
                        }
                    }
                }

            }
            else
            {
                _repeat--;
            }

            return _curBit;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _chunkReader?.Dispose();
                    _chunkReader = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UEFTapeStreamer()
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
    }
}
