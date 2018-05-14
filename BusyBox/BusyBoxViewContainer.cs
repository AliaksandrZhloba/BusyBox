using System;
using System.Windows;
using System.Windows.Controls;


namespace BusyBox
{
    public class BusyBoxViewContainer : ContentControl
    {
        static BusyBoxViewContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyBoxViewContainer), new FrameworkPropertyMetadata(typeof(BusyBoxViewContainer)));
        }
    }
}
