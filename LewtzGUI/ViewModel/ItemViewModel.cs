using ItemRoller.Data_Structure;
using System;
using System.Collections.ObjectModel;

namespace LewtzGUI.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private Component _ViewItem;
        public Component ViewItem
        {
            get
            {
                return _ViewItem;
            }

            set
            {
                _ViewItem = value;
                OnPropertyChanged("ViewItem");
            }
        }

        public string ItemName
        {
            get
            {
                return ViewItem.Name;
            }
        }

        public ItemViewModel(Component comp)
        {
            ViewItem = comp;
            OnPropertyChanged("ViewItem");

        }
    }
}
