using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ElkCSharpSettings
{
    public class Settings
    {
        public IList<KeyMap> KeyMappings { get; init; }

        public Settings()
        {
            KeyMappings = new List<KeyMap>();
        }


    }
}
