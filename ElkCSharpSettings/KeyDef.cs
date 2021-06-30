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
        private static KeyDef _empty = new KeyDef() { WindowsKey = Key.None, KeyMatrices = new KeyMatrix[0] };
        public static KeyDef Empty { get { return _empty; } }
        public Key WindowsKey { get; init; }
        public TriState Shift { get; init; }
        public TriState Ctrl { get; init; }
        public KeyMatrix[] KeyMatrices {get; init;}

        public static bool operator ==(KeyDef a, KeyDef b) => a.WindowsKey == b.WindowsKey;
        public static bool operator !=(KeyDef a, KeyDef b) => a.WindowsKey != b.WindowsKey;

        public override bool Equals(object obj)
        {
            return ((obj as KeyDef?) ?? _empty) == this;
        }
        public override int GetHashCode()
        {
            return WindowsKey.GetHashCode();
        }
    }

}
