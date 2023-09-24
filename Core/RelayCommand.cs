using System;
using System.Windows.Input;

namespace EmptyBank.Core
{
    internal class RelayCommand : ICommand
    {
        public Action<object> execute;
        public Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);    
        }

        public void OnExecute(object parameter)
        {
            var values = (object[]) parameter;
            var password = (string)values[0];
            var repeatPassword = (string)values[1];
        }

    }
}
