using ItemRoller.Data_Structure;
using LewtzGUI.Data_Access;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    class ItemRollerViewModel : ViewModelBase
    {
        TableRepository baseRepoToRollFrom;
        public ItemRollerViewModel(TableRepository repo)
        {
            baseRepoToRollFrom = repo;
        }

        private string _HoardName = "new hoard";
        public string HoardName
        {
            get
            {
                return _HoardName;
            }

            set
            {
                _HoardName = value;
                OnPropertyChanged("HoardName");
            }
        }

        private int _Level = 1;
        public int Level
        {
            get
            {
                return _Level;
            }

            set
            {
                _Level = value;
                OnPropertyChanged("Level");
            }
        }

        private List<int> _SelectableLevels;
        public List<int> SelectableLevels
        {
            get
            {
                if (_SelectableLevels == null)
                {
                    _SelectableLevels = new List<int>();
                }
                return _SelectableLevels;
            }
        }

        private ObservableCollection<ItemViewModel> _LootBag;
        public ObservableCollection<ItemViewModel> LootBag
        {
            get
            {
                if (_LootBag == null)
                {
                    _LootBag = new ObservableCollection<ItemViewModel>();
                }
                return _LootBag;
            }
        }

        RelayCommand _lootCommand;
        public ICommand LootCommand
        {
            get
            {
                if(_lootCommand == null)
                {
                    _lootCommand = new RelayCommand(
                        param => RollLoot(),
                        param => true);
                }
                return _lootCommand;
            }
        }

        public void RollLoot()
        {
            var baseTable = baseRepoToRollFrom.DatabaseContext.GetTableFromString("treasure table");
            var loot = baseRepoToRollFrom.RollTableLoot(baseTable);

            LootBag.Clear();
            foreach(var item in loot)
            {
                LootBag.Add(new ItemViewModel(item));
            }
            OnPropertyChanged("LootBag");
        }

        public void OnLootBagChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (ItemViewModel item in e.NewItems)
                {

                }

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (ItemViewModel itemroller in e.OldItems) { }
        }

        RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(
                       param => Close(),
                       param => CanClose()
                       );
                }
                return _closeCommand;
            }
        }
    }
}
