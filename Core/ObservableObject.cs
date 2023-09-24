using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmptyBank.Core
{
    internal class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool canExecute(object parameter)
        {
            var data = parameter as object[];
            var password = data[0] as string;
            var repeatPassword = data[1] as string;

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) { return false; }
            else { return true; }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
