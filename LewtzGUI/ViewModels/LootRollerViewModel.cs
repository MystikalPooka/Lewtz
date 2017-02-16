using ItemRoller.Data_Structure;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LewtzGUI.ViewModels
{
    public class LootRollerViewModel : ViewModelBase
    {
        public LootRollerViewModel()
        {
            var JsonLoader = new TableRepository();
            LoadedRepositories.Add("DND 3.5", new TableRepository());
        }

        public Dictionary<string,TableRepository> LoadedRepositories
        {
            get;
            set;
        }
        public ObservableCollection<Component> LootBagComponents
        {
            get;
            set;
        }
    }
}
