using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFStuff;

namespace ElkCSharpSettings
{

    public class KeyMap : ObservableObject
    {
        private Settings _settings;

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _description;
        public string Description { 
            get => _description;
            set => Set(ref _description, value);
        }

        private bool _current;
        public bool Current {
            get => _current;
            set {
                if (value != _current)
                {
                    if (value == false)
                        _current = false;
                    else
                        SetCurrent();

                    RaisePropertyChangedEvent();
                }
            }
        }

        public void SetCurrent()
        {
            _settings.KeyMappings.Where(o => o != this).ToList().ForEach(o => o.SetCurrent(false));
            SetCurrent(true);
        }

        protected void SetCurrent(bool cur)
        {
            _current = cur;
            RaisePropertyChangedEvent(nameof(Current));
        }

        public IList<KeyDef> Keys { get; set; }

        public KeyMap(Settings settings)
        {
            _settings = settings;
            Keys = new List<KeyDef>();
        }

    }
}
