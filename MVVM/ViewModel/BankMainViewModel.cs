using EmptyBank.Core;
using EmptyBank.Core.Service;
using EmptyBank.MVVM.Model;
using System.Windows;

namespace EmptyBank.MVVM.ViewModel
{
    internal class BankMainViewModel : ObservableObject
    {
        public RelayCommand Refresh { get; set; }  
        ServerModel model;
        private string _greetingsText;
        public string GreetingsText
        {
            get { return _greetingsText; }
            set { _greetingsText = value; OnPropertyChanged(); }
        }

        private string _cardBalance;
        public string CardBalance
        {
            get { return _cardBalance; }
            set { _cardBalance = value; OnPropertyChanged(); }
        }

        private string _cardNumber;
        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; OnPropertyChanged(); }
        }

        private string _cardNumberTip;
        public string CardNumberTip
        {
            get { return _cardNumberTip; }
            set { _cardNumberTip = value; OnPropertyChanged(); }
        }

        private string _cardCVC;
        public string CardCVC
        {
            get { return _cardCVC; }
            set { _cardCVC = value; OnPropertyChanged(); }
        }

        private string _cashbackBalance;
        public string CashbackBalance
        {
            get { return _cashbackBalance; }
            set { _cashbackBalance = value; OnPropertyChanged(); }
        }

        private Visibility _modalTitle = Visibility.Hidden;
        public Visibility ModalTitle
        {
            get { return _modalTitle; }
            set { _modalTitle = value; OnPropertyChanged(); }
        }

        public RelayCommand ShowModalTitle { get; set; }
        public RelayCommand HideModalTitle { get; set; }

        public BankMainViewModel() {
            ShowModalTitle = new RelayCommand(sender => ShowTitle(sender));
            HideModalTitle = new RelayCommand(sender => HideTitle(sender));
            Refresh = new RelayCommand(sender => RefreshData(sender));
            ServerModel model = new ServerModel();
            GreetingsText = $"Здравствуйте, {model.Login}!";
            string CardNum = model.CardNumber.ToString();

            CardBalance = model.Balance.ToString() + " ₽";
            CardNumber = $"• • {CardNum.Substring(CardNum.Length - 4)}";
            CardNumberTip = model.CardNumber.ToString();
            CardCVC = model.Cvc.ToString();

            CashbackBalance = model.Bonuses.ToString() + " ₽";
        }

        private void ShowTitle(object sender)
        {
            ModalTitle = Visibility.Visible;
        }

        private void HideTitle(object sender)
        {
            ModalTitle = Visibility.Hidden;
        }

        private void RefreshData(object sender)
        {
            ServerModel model = new ServerModel();
            AuthService authService = new AuthService();
            CardBalance = authService.GetBalance() + " ₽";
            CashbackBalance = authService.GetCashback() + " ₽";
        }
    }
}
