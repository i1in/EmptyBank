using EmptyBank.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyBank.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        /* Все используемые команды*/
        
        public RelayCommand ShowAuthView { get; set; }
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

        public AuthViewModel AuthViewPage { get; set; }

        public SignViewModel SignViewPage { get; set; }

        public static MainViewModel Instance { get; set; }

        public MainViewModel()
        {
            Instance = this;
            AuthViewPage = new AuthViewModel();
            SignViewPage = new SignViewModel();
            CurrentView = AuthViewPage;
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            ShowAuthView = new RelayCommand(n => { CurrentView = AuthViewPage; });
        }
    }
}
