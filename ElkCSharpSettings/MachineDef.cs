using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharpSettings
{
    public class MachineDef
    {
        public string Name { get; init; }
        public IList<RomDef> RomDefs {get; init;}

        public MachineDef()
        {
            RomDefs = new List<RomDef>();
        }
    }
}
