using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStuff;

namespace ElkCSharp.ViewModel
{
    public class LEDModel : ObservableObject
    {
        bool _lit = false;
        string _name;
        public string Name { 
            get => _name;
            set => Set(ref _name, value);
        }
        public bool Lit { 
            get => _lit;
            set => Set(ref _lit, value);
        }
    }
}
