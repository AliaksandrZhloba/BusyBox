using System;
using System.Windows;

using BusyBox.Demo.ViewModels;
using BusyBox.Demo.Views;


namespace BusyBox.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var view = new DemoView(new DemoViewModel());
            BBView.Content = view;
        }
    }
}
