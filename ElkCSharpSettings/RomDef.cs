using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharpSettings
{
    public struct RomDef
    {
        public int Number { get; init; }
        public string Load { get; init; }
        public bool WriteEnable { get; init; }
    }
}
