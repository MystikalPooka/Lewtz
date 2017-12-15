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

        TableRepository baseRepoToRollFrom;
        public ItemRollerViewModel(TableRepository repo)
        {
            baseRepoToRollFrom = repo;
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
