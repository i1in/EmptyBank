using EmptyBank.Core;
using EmptyBank.MVVM.View;
using EmptyBank.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyBank.MVVM.ViewModel
{
    internal class SignViewModel: ObservableObject
    {
        public RelayCommand ShowAuthView { get; set; }

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

        public AuthViewModel AuthViewPage { get; set; }
        public SignViewModel() {
            ShowAuthView = new RelayCommand(n => { CurrentView = new AuthViewModel(); });        
        }
    }
}
