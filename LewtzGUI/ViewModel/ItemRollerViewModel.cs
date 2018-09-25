using ItemRoller.Visitors;
using LewtzGUI.Data_Access;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace LewtzGUI.ViewModel
{
    class ItemRollerViewModel : ViewModelBase
    {
        TableRepository baseRepoToRollFrom;
        public ItemRollerViewModel(TableRepository repo)
        {
            baseRepoToRollFrom = repo;
        }

        private Brush _LootBackgroundBrush;
        public Brush LootBackgroundBrush
        {
            get
            {
                if (_LootBackgroundBrush == null)
                {
                    if(LootBackColor == null)
                        LootBackColor = Colors.LightSlateGray;
                    else
                        _LootBackgroundBrush = new SolidColorBrush(LootBackColor);
                }
                return _LootBackgroundBrush;
            }
            set
            {
                _LootBackgroundBrush = value;
                OnPropertyChanged("LootBackgroundBrush");
            }
        }

        private Color _LootBackColor;
        public Color LootBackColor
        {
            get => _LootBackColor;
            set
            {
                _LootBackColor = value;
                LootBackgroundBrush = new SolidColorBrush(_LootBackColor);
                OnPropertyChanged("LootBackColor");
            }
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

        private BindingList<ItemViewModel> _LootBag;
        public BindingList<ItemViewModel> LootBag
        {
            get
            {
                if (_LootBag == null)
                {
                    _LootBag = new BindingList<ItemViewModel>();
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
                        this.OnPropertyChanged("LootBag");
                    }
                } 
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
