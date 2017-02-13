using System.ComponentModel;

namespace LewtzGUI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, System.IDisposable
    {
        protected ViewModelBase() {}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        public virtual void OnDispose() {}
    }
}
