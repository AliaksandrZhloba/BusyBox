using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace BusyBox.ViewModels
{
    public class BaseBusyBoxViewModel : IBusyBoxViewModel, INotifyPropertyChanged
    {
        private BusyBox.BaseBusyBox _busyBox;

        public bool IsBusy => BusyBox != null;

        public BusyBox.BaseBusyBox BusyBox
        {
            get { return _busyBox;}
            set
            {
                _busyBox = value;
                OnPropertyChanged(nameof(BusyBox));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
