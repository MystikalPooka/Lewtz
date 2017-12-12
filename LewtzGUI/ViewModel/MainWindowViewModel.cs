using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace LewtzGUI.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<ItemRollerViewModel> _ItemRollers;
        public ObservableCollection<ItemRollerViewModel> ItemRollers
        {
            get
            {
                if (_ItemRollers == null)
                {
                    _ItemRollers = new ObservableCollection<ItemRollerViewModel>();
                    _ItemRollers.CollectionChanged += this.OnItemRollersChanged;
                }
                return _ItemRollers;
            }
        }
        void OnItemRollersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (ItemRollerViewModel itemroller in e.NewItems)
                    itemroller.RequestClose +=() => this.ItemRollers.Remove(sender as ItemRollerViewModel);

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (ItemRollerViewModel itemroller in e.OldItems)
                    itemroller.RequestClose -= () => this.ItemRollers.Remove(sender as ItemRollerViewModel);
        }


        void OnItemRollerRequestClose(object sender, EventArgs e)
        {
            this.ItemRollers.Remove(sender as ItemRollerViewModel);
        }
    }
}
