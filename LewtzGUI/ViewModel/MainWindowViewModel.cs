using LewtzGUI.Data_Access;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        TableRepository MainDBContext;

        ObservableCollection<ItemRollerViewModel> _ItemRollers;
        public ObservableCollection<ItemRollerViewModel> ItemRollers
        {
            get
            {
                if (_ItemRollers == null)
                {
                    _ItemRollers = new ObservableCollection<ItemRollerViewModel>();
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
            }
            this.ItemRollers.Add(new ItemRollerViewModel(MainDBContext));
        }
    }
}
