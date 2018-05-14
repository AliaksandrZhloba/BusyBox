using System;
using System.Windows.Controls;

using BusyBox.Demo.ViewModels;


namespace BusyBox.Demo.Views
{
    /// <summary>
    /// Interaction logic for DemoView.xaml
    /// </summary>
    public partial class DemoView : UserControl
    {
        public DemoView(DemoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
