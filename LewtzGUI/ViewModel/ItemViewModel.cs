using ItemRoller.Data_Structure;
using System;
using System.Collections.ObjectModel;

namespace LewtzGUI.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private Component _ViewItem;

        public string ItemName
        {
            get
            {
                return _ViewItem.Name;
            }
        }

        public string ItemBook
        {
            get
            {
                return _ViewItem.Book;
            }
        }

        public int NumCopper
        {
            get
            {
                return ItemCost % 10; 
            }
        }

        public int NumSilver
        {
            get
            {
                return (ItemCost / 10 ) % 10;
            }
        }

        public int NumGold
        {
            get
            {
                return (int)Math.Floor(ItemCost / 100.0);
            }
        }

        public int ItemCost
        {
            get
            {
                if (_ViewItem is Item)
                    return (_ViewItem as Item).Cost;
                else return 0;
            }
        }

        private ObservableCollection<Component> _ItemAbilities;
        public ObservableCollection<Component> ItemAbilities
        {
            get
            {
                if(_ViewItem is MagicItem)
                {
                    _ItemAbilities = new ObservableCollection<Component>((_ViewItem as MagicItem).Abilities);
                }
                return _ItemAbilities;
            }
        }

        public ItemViewModel(Component comp)
        {
            _ViewItem = comp;
        }
    }
}
