using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElkCSharpSettings
{
    public struct KeyDef
    {
        private static KeyDef _empty = new KeyDef() { Key = Key.None, Col = -1, Row = -1 };
        public static KeyDef Empty { get { return _empty; } }
        public Key Key { get; init; }
        public int Row { get; init; }
        public int Col { get; init; }

        public static bool operator ==(KeyDef a, KeyDef b) => a.Key == b.Key;
        public static bool operator !=(KeyDef a, KeyDef b) => a.Key != b.Key;

        public override bool Equals(object obj)
        {
            return ((obj as KeyDef?) ?? _empty) == this;
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }

}
