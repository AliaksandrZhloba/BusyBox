using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

using BusyBox.Commands;
using BusyBox.Helpers;
using BusyBox.ViewModels;


namespace BusyBox
{
    public class CancellationBusyBox : BaseBusyBox, INotifyPropertyChanged
    {
        private const string DefaultCancelText = "Cancel";
        private string _message;
        private string _cancelText;


        public string Message
        {
            get { return _message;}
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string CancelText
        {
            get { return _cancelText; }
            set
            {
                _cancelText = value;
                OnPropertyChanged(nameof(CancelText));
            }
        }

        public ICommand Cancel { get; }
        public CancellationTokenSource BusyCts;


        public CancellationBusyBox(BaseBusyBoxViewModel viewModel, string message)
            : this(viewModel, ResourceHelper.Get<ControlTemplate>("CancellationBusyBoxTemplate"), message, DefaultCancelText, new CancellationTokenSource())
        {
        }

        public CancellationBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template, string message)
            : this(viewModel, template, message, DefaultCancelText, new CancellationTokenSource())
        {
        }

        public CancellationBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template, string message, string cancelText)
            : this(viewModel, template, message, cancelText, new CancellationTokenSource())
        {
        }

        public CancellationBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template, string message, string cancelText, CancellationTokenSource busyCts)
            : base(viewModel, template)
        {
            Message = message;
            CancelText = cancelText;
            BusyCts = busyCts;
            Cancel = new RelayCommand(() => BusyCts.Cancel());
        }


        public void UpdateMessage(string message)
        {
            Message = message;
            OnPropertyChanged(nameof(Message));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
