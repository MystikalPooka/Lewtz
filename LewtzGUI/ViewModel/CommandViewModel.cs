using System;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            this.Command = command;
        }
        public ICommand Command { get; private set; }
    }
}
