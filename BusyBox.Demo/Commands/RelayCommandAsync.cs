using System;
using System.Threading.Tasks;
using System.Windows.Input;


namespace BusyBox.Demo.Commands
{
    public class RelayCommandAsync : ICommand
    {
        private bool _isBlocked = false;
        private readonly Func<Task> _execute;

        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;


        public RelayCommandAsync(Func<Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return
                !_isBlocked &&
                (_canExecute == null || _canExecute(parameter));
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public async void Execute(object parameter)
        {
            if (_isBlocked)
                return;

            _isBlocked = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            try
            {
                await _execute();
            }
            finally
            {
                _isBlocked = false;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public class RelayCommandAsync<T> : ICommand
    {
        private bool _isBlocked = false;
        private readonly Func<T, Task> _execute;

        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged;


        public RelayCommandAsync(Func<T, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return
                !_isBlocked &&
                (_canExecute == null || _canExecute(parameter));
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public async void Execute(object parameter)
        {
            if (_isBlocked)
                return;

            _isBlocked = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            try
            {
                await _execute((T)parameter);
            }
            finally
            {
                _isBlocked = false;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
