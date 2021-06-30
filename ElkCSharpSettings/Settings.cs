using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ElkCSharpSettings
{
    public class Settings
    {
        public ObservableCollection<KeyMap> KeyMappings { get; init; }
        public ObservableCollection<MachineDef> MachineDefs { get; init; }

        public Settings()
        {
            KeyMappings = new ObservableCollection<KeyMap>();
            MachineDefs = new ObservableCollection<MachineDef>();
        }


    }
}
