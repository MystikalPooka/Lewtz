using System;
using System.Windows;

namespace LewtzGUI.Commands
{
    public class ShowDialogCommand : OpenWindowCommand
    {
        private Action _PreOpenDialogAction;
        private Action<bool?> _PostOpenDialogAction;

        public ShowDialogCommand(Action<bool?> postDialogAction)
        {
            _PostOpenDialogAction = postDialogAction ?? throw new ArgumentNullException("postDialogAction");
        }

        public ShowDialogCommand(Action<bool?> postDialogAction, Action preDialogAction)
            : this(postDialogAction)
        {
            _PreOpenDialogAction = preDialogAction ?? throw new ArgumentNullException("preDialogAction");
        }

        protected override void OpenWindow(Window wnd)
        {
            //If there is a pre dialog action then invoke that.
            _PreOpenDialogAction?.Invoke();

            //Show the dialog
            bool? result = wnd.ShowDialog();

            //Invoke the post open dialog action.
            _PostOpenDialogAction(result);
        }
    }
}