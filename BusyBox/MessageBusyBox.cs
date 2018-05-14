using System;
using System.ComponentModel;
using System.Windows.Controls;

using BusyBox.Helpers;
using BusyBox.ViewModels;


namespace BusyBox
{
    public class MessageBusyBox : BaseBusyBox, INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        public MessageBusyBox(BaseBusyBoxViewModel viewModel)
            : this(viewModel, ResourceHelper.Get<ControlTemplate>("MessageBusyBoxTemplate"), string.Empty)
        {
        }

        public MessageBusyBox(BaseBusyBoxViewModel viewModel, string message)
            : this(viewModel, ResourceHelper.Get<ControlTemplate>("MessageBusyBoxTemplate"), message)
        {
        }

        public MessageBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template)
            : this(viewModel, template, string.Empty)
        {
        }

        public MessageBusyBox(BaseBusyBoxViewModel viewModel, ControlTemplate template, string message)
            : base(viewModel, template)
        {
            Message = message;
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
