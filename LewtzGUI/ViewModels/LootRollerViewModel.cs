using ItemRoller.Data_Structure;
using System.Collections.ObjectModel;

namespace LewtzGUI.ViewModels
{
    public class LootRollerViewModel : ViewModelBase
    {
        public ObservableCollection<Component> LootBagComponents
        {
            get;
            set;
        }
    }
}
