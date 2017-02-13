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
        public static bool GetViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(ViewModelProperty);
        }

        public static void SetUpViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(ViewModelProperty, value);
        }

        public static readonly DependencyProperty ViewModelProperty =       
            DependencyProperty.RegisterAttached("ViewModel", typeof(bool),
                                                typeof(ViewModelLocator), new
                                                PropertyMetadata(false, ViewModelChanged));
        

        public static void ViewModelChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dObj)) return;

            var viewType = dObj.GetType();

            string viewTypeName = viewType.FullName;
            viewTypeName = viewTypeName.Replace(".Views.", ".ViewModel.");

            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);

            ((FrameworkElement)dObj).DataContext = viewModel;s
        }
    }
}
