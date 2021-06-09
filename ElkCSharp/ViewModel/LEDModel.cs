using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharp.ViewModel
{
    public class LEDModel : ObservableObject
    {
        bool _lit = false;
        public string Name { get; init; }
        public bool Lit { 
            get
            {
                return _lit;
            } 
            set
            {
                if (value != _lit)
                {
                    _lit = value;
                    RaisePropertyChangedEvent();
                }
            }
        }
    }
}
