using System;
using System.Threading;
using System.Windows.Controls;
using BusyBox.ViewModels;


namespace BusyBox
{
    public abstract class BaseBusyBox : IDisposable
    {
        private readonly BaseBusyBoxViewModel _viewModel;
        private readonly BusyBox.BaseBusyBox _prevBusyBox;

        public ControlTemplate Template { get; }


        public BaseBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template)
        {
            Template = template;

            _viewModel = viewModel;
            _prevBusyBox = _viewModel.BusyBox;
            _viewModel.BusyBox = this;
        }


        public void Dispose()
        {
            _viewModel.BusyBox = _prevBusyBox;
        }
    }
}
