using LewtzGUI.Data_Access;
using System.Windows.Input;
using System.ComponentModel;
using LewtzGUI.Commands;

namespace LewtzGUI.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private TableRepository _mainDBContext;
        public TableRepository MainDBContext
        {
            get
            {
                return _mainDBContext;
            }

            set
            {
                _mainDBContext = value;
                OnPropertyChanged("MainDBContext");
            }
        }

        private ItemViewModel _RepoView;
        public ItemViewModel RepoView
        {
            get
            {
                return _RepoView;
            }

            set
            {
                _RepoView = value;
                OnPropertyChanged("RepoView");
            }
        }

        private BindingList<ItemRollerViewModel> _ItemRollers;
        public BindingList<ItemRollerViewModel> ItemRollers
        {
            get
            {
                if (_ItemRollers == null)
                {
                    _ItemRollers = new BindingList<ItemRollerViewModel>();
                }
                return _ItemRollers;
            }
        }

        RelayCommand _newRollerCommand;
        public ICommand AddItemRoller
        {
            get
            {
                if (_newRollerCommand == null)
                {
                    _newRollerCommand = new RelayCommand(
                       param => AddNewItemRoller(),
                       param => true
                       );
                }
                return _newRollerCommand;
            }
        }

        RelayCommand _removeLastRoller;
        public ICommand RemoveItemRoller
        {
            get
            {
                if (_removeLastRoller == null)
                {
                    _removeLastRoller = new RelayCommand(
                       param => ItemRollers.RemoveAt(ItemRollers.Count - 1),
                       param => (ItemRollers.Count > 0)
                       );
                }
                return _removeLastRoller;
            }
        }

        RelayCommand _rollAllLootCommand;
        public ICommand RollAllLootCommand
        {
            get
            {
                if (_rollAllLootCommand == null)
                {
                    _rollAllLootCommand = new RelayCommand(
                       param => RollAllLoot(),
                       param => (ItemRollers.Count > 0)
                       );
                }
                return _rollAllLootCommand;
            }
        }

        void RollAllLoot()
        {
            foreach(var hoard in _ItemRollers)
            {
                hoard.RollLoot();
            }
        }

        void AddNewItemRoller()
        {
            if (MainDBContext == null)
            {
                MainDBContext = new TableRepository();
                RepoView = new ItemViewModel(MainDBContext.DatabaseContext.GetTableFromString("magic base"));
            }
            this.ItemRollers.Add(new ItemRollerViewModel(MainDBContext));
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

        private OpenWindowCommand _openWindowCommand = new OpenWindowCommand();
        public OpenWindowCommand OpenWindowCommand {
            get
            {
                return _openWindowCommand;
            }

            private set
            {

            }
        }
        public ShowDialogCommand ShowDialogCommand { get; private set; }
    }
}
