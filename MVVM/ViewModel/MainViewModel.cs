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
        public RelayCommand MoveWindow {  get; set; }
        public RelayCommand ShutdownWindow { get; set; }
        public RelayCommand HideWindow {  get; set; }
        
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

        public MainViewModel()
        {
            AuthViewPage = new AuthViewModel();
            SignViewPage = new SignViewModel();
            CurrentView = AuthViewPage;
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindow = new RelayCommand(n => { Application.Current.MainWindow.DragMove(); });
            ShutdownWindow = new RelayCommand(n => { Application.Current.Shutdown();});
            HideWindow = new RelayCommand(n => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            ShowAuthView = new RelayCommand(n => { CurrentView = AuthViewPage; });
        }
    }
}
