using ItemRoller.Data_Structure;
using LewtzGUI.Data_Access;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    class ItemRollerViewModel : ViewModelBase
    {
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

        TableRepository baseRepoToRollFrom;
        public ItemRollerViewModel(TableRepository repo)
        {
            baseRepoToRollFrom = repo;
        }

        private ObservableCollection<Component> _lootBag;
        public ObservableCollection<Component> LootBag
        {
            get
            {
                return _lootBag;
            }

            protected set
            {
                _lootBag = value;
                OnPropertyChanged("LootBag");
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
