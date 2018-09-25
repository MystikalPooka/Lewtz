using LewtzGUI.ViewModel;
using System.Windows;
using Xceed.Wpf.Toolkit.Primitives;

namespace LewtzGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
