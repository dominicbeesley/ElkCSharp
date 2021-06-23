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
        public struct KeyDef
        {
            private static KeyDef _empty = new KeyDef() { Key = Key.None, Col = -1, Row = -1 };
            public static KeyDef Empty { get { return _empty; } }
            public Key Key { get; init; }
            public int Row { get; init; }
            public int Col { get; init; }
        }


        public string Name { get; init; }

        public IList<KeyDef> Keys { get; init; }

        public KeyMap()
        {
            Keys = new List<KeyDef>();
        }

    }
}
