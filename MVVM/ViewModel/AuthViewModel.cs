using EmptyBank.Core;
using EmptyBank.Core.Service;
using EmptyBank.MVVM.View;
using EmptyBank.MVVM.ViewModel;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyBank.MVVM.ViewModel
{
    internal class AuthViewModel : ObservableObject
    {
        public RelayCommand ShowMainView {  get; set; }
        public RelayCommand ShowSignView { get; set; }
        public SignViewModel SignViewPage { get; set; }

        private string _loginText = string.Empty;
        public string LoginText
        {
            get { return _loginText; }
            set { _loginText = value; OnPropertyChanged(); }
        }

        private string _passText;
        public string PassText
        {
            get { return _passText; }
            set { _passText = value; OnPropertyChanged(); }
        }

        private string _authLoginWarningText;
        public string AuthLoginWarningText
        {
            get { return _authLoginWarningText; }
            set { _authLoginWarningText = value; OnPropertyChanged(); }
        }

        private string _authPasswordWarningText;
        public string AuthPasswordWarningText
        {
            get { return _authPasswordWarningText; }
            set { _authPasswordWarningText = value; OnPropertyChanged(); }
        }

        private string _authLoginBrush;
        public string AuthLoginBrush
        {
            get { return _authLoginBrush; }
            set { _authLoginBrush = value; OnPropertyChanged(); }
        }

        private string _authPasswordBrush;
        public string AuthPasswordBrush
        {
            get { return _authPasswordBrush; }
            set { _authPasswordBrush = value; OnPropertyChanged(); }
        }

        private bool _authCheckBox;
        public bool AuthCheckBox
        {
            get { return _authCheckBox; }
            set { _authCheckBox = value; OnPropertyChanged(); }
        }

        public void CloseWindow()
        {
            var thisWindow = Application.Current.Windows[0];
            if (thisWindow != null) thisWindow.Close();
        }

        public AuthViewModel() {
            ShowMainView = new RelayCommand(sender => AuthButton(sender));
            ShowSignView = new RelayCommand(sender => ShowSignUpView(sender));

            AuthLoginBrush = Brushes.White.ToString();
            AuthPasswordBrush = Brushes.White.ToString();
        }

        private void AuthButton(object sender)
        {
            AuthService authService = new AuthService();
            PasswordBox password = sender as PasswordBox;
            if (password != null) { PassText = password.Password; }

            if (LoginText.Length < 3 || LoginText.Length > 16)
            {
                AuthLoginWarningText = "Длина логина должна составлять от 3 до 16 символов";
                AuthLoginBrush = Brushes.Red.ToString();
                return;
            }
            else { AuthLoginWarningText = ""; AuthLoginBrush = Brushes.Green.ToString(); }

            if (PassText.Length < 3 || PassText.Length > 16)
            {
                AuthPasswordWarningText = "Длина пароля должна составлять от 3 до 16 символов";
                AuthPasswordBrush = Brushes.Red.ToString();
                return;
            }
            else { AuthPasswordWarningText = ""; AuthPasswordBrush = Brushes.Green.ToString(); }

            if (!authService.IsExists(LoginText))
            {
                AuthLoginWarningText = "Пользователя с данным логином не существует";
                AuthLoginBrush = Brushes.Red.ToString();
                return;
            }
            else { AuthLoginWarningText = ""; AuthLoginBrush = Brushes.Green.ToString(); }

            if (!authService.Find(LoginText, PassText))
            {
                AuthPasswordWarningText = "Неправильный пароль";
                AuthPasswordBrush = Brushes.Red.ToString();
                return;
            }
            else { AuthPasswordWarningText = ""; AuthPasswordBrush = Brushes.Green.ToString(); }

            if (AuthCheckBox)
                Properties.Settings.Default.IsRemember = AuthCheckBox;
                Properties.Settings.Default.Save();


            var openBankWindow = new BankWindow();
            var viewmodel = openBankWindow.DataContext as BankViewModel;

            openBankWindow.Show();
            CloseWindow();
        }

        private void ShowSignUpView(object sender)
        {
            MainViewModel.Instance.CurrentView = new SignViewModel();
        }
    }
}
