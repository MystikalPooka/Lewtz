using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LewtzGUI.VML
{
    //Referenced from Tutorials point
    //https://www.tutorialspoint.com/mvvm/mvvm_hooking_up_viewmodel.htm
    //
    public static class ViewModelLocator
    {
        public static bool GetAutoViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoViewModelProperty);
        }

        public static void SetAutoViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoViewModelProperty =       
            DependencyProperty.RegisterAttached("AutoViewModel", typeof(bool),
                                                typeof(ViewModelLocator), new
                                                PropertyMetadata(false, AutoViewModelChanged));
        

        public static void AutoViewModelChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(dObj)) return;

            var viewType = dObj.GetType();

            string viewTypeName = viewType.FullName;
            viewTypeName = viewTypeName.Replace(".Views.", ".ViewModel.");

            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);

            ((FrameworkElement)dObj).DataContext = viewModel;
        }
    }
}
