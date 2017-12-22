using ItemRoller.Data_Structure;
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
