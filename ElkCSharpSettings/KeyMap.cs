using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElkCSharpSettings
{

    public class KeyMap
    {
        public string Name { get; init; }
        public string Description { get; init; }

        public IList<KeyDef> Keys { get; init; }

        public KeyMap()
        {
            Keys = new List<KeyDef>();
        }

    }
}
