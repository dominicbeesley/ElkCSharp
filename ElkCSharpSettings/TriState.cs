using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharpSettings
{
    /// <summary>
    /// represents a value that can be compared to a boolean as either True==True, False==False, Any==True, Any==False
    /// </summary>
    public enum TriState
    {
        False=0,
        True=1,
        Any=-1
    }

}
