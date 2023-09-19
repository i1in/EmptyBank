using EmptyBank.Core;
using EmptyBank.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBank.MVVM.ViewModel
{
    internal class AuthViewModel : ObservableObject
    {
        public RelayCommand ShowSignView { get; set; }

        public object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public SignViewModel SignViewPage { get; set; }
        public AuthViewModel() {
            SignViewPage = new SignViewModel();
            ShowSignView = new RelayCommand(n => { CurrentView = SignViewPage; });
        }
    }
}
