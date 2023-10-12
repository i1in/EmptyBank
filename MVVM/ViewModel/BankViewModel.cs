using EmptyBank.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyBank.MVVM.ViewModel
{
    internal class BankViewModel : ObservableObject
    {
        public RelayCommand ShowBankView { get; set; }
        public RelayCommand ShowBillView { get; set; }
        public RelayCommand ShowHistoryView { get; set; }
        public RelayCommand ShowPersonalView { get; set; }

        public object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public BankMainViewModel BankMainViewPage { get; set; }
        public BankBillViewModel BankBillViewPage { get; set; }
        public BankHistoryViewModel BankHistoryViewPage { get; set; }
        public PersonalViewModel PersonalViewPage { get; set; }

        public static BankViewModel Instance {  get; set; }

        public BankViewModel() {
            Instance = this;

            BankMainViewPage = new BankMainViewModel();
            BankBillViewPage = new BankBillViewModel();
            BankHistoryViewPage = new BankHistoryViewModel();
            PersonalViewPage = new PersonalViewModel();

            CurrentView = BankMainViewPage;
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            ShowBankView = new RelayCommand(n => { CurrentView = BankMainViewPage; });
            ShowBillView = new RelayCommand(n => { CurrentView = BankBillViewPage; });
            ShowHistoryView = new RelayCommand(n => { CurrentView = BankHistoryViewPage; });
            ShowPersonalView = new RelayCommand(n => { CurrentView =  PersonalViewPage; });
        }
    }
}
