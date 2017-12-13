using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    class ItemRollerViewModel : ViewModelBase
    {
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
