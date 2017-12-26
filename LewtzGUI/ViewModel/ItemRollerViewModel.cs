using ItemRoller.Visitors;
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

        private int _NumRolls = 1;
        public int NumRolls
        {
            get
            {
                return _NumRolls;
            }

            set
            {
                _NumRolls = value;
                OnPropertyChanged("NumRolls");
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

        public int ItemCost;

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
                return (ItemCost / 10) % 10;
            }
        }

        public int NumGold
        {
            get
            {
                return (ItemCost / 100) % 10;
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
            var dbContext = baseRepoToRollFrom.DatabaseContext;
            var baseTable = dbContext.GetTableFromString("treasure table");
            LootBag.Clear();
            for (int i = 0; i < NumRolls; ++i)
            {
                var loot = baseRepoToRollFrom.RollTableLoot(baseTable);

                foreach (var item in loot)
                {
                    if (item.Name != "")
                    {
                        item.Accept(new BuildItemVisitor(dbContext));
                        LootBag.Add(new ItemViewModel(item));
                    }
                } 
            }
            OnPropertyChanged("LootBag");
        }

        public void OnLootBagChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (ItemViewModel item in e.NewItems)
                {
                    ItemCost += item.ItemCost;
                }

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (ItemViewModel item in e.OldItems)
                {
                    ItemCost -= item.ItemCost;
                }
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
