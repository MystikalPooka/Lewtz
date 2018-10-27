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

        public int ItemProbability
        {
            get
            {
                return _ViewItem.Probability;
            }
        }

        public string ItemTypes
        {
            get
            {
                return _ViewItem.Types.ToString();
            }
        }

        public ObservableCollection<ItemViewModel> Children
        {
            get
            {
                
                if (_ViewItem is Table)
                {
                    var viewTable = (Table)_ViewItem;
                    ObservableCollection<ItemViewModel> children = new ObservableCollection<ItemViewModel>();
                    foreach(var comp in viewTable.GetChildren())
                    {
                        children.Add(new ItemViewModel(comp));
                    }
                    return children;

                }
                else return null;
            }
        }

        public ItemViewModel(Component comp)
        {
            _ViewItem = comp;
            OnPropertyChanged("ViewItem");
        }
    }
}
