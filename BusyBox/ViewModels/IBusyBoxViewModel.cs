using System;


namespace BusyBox.ViewModels
{
    public interface IBusyBoxViewModel
    {
        bool IsBusy { get; }
        BaseBusyBox BusyBox { get; set; }
    }
}
