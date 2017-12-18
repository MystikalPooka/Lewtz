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

        private string _Name = "new hoard";
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                OnPropertyChanged("Name");
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

        ObservableCollection<Component> _LootBag;
        public ObservableCollection<Component> LootBag
        {
            get
            {
                if (_LootBag == null)
                {
                    _LootBag = new ObservableCollection<Component>();
                    _LootBag.CollectionChanged += this.OnLootBagChanged;
                }
                return _LootBag;
            }
        }

        void OnLootBagChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //if (e.NewItems != null && e.NewItems.Count != 0)
            //    foreach (Component item in e.NewItems)
            //        itemroller.RequestClose += () => this.ItemRollers.Remove(sender as ItemRollerViewModel);

            //if (e.OldItems != null && e.OldItems.Count != 0)
            //    foreach (ItemRollerViewModel itemroller in e.OldItems)
            //        itemroller.RequestClose -= () => this.ItemRollers.Remove(sender as ItemRollerViewModel);
        }



        RelayCommand _lootCommand;
        public ICommand lootCommand
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
            var lootBag = baseRepoToRollFrom.RollTableLoot(baseTable);
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
