using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzGUI.ViewModel
{
    public class LootBagViewModel
    {
        private List<Component> _LootBag;
        public List<Component> LootBag
        {
            get
            {
                if (_LootBag == null) _LootBag = new List<Component>();
                return _LootBag;
            }
        }
    }
}
