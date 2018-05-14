using System;
using System.Windows.Input;


namespace BusyBox.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;


        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
