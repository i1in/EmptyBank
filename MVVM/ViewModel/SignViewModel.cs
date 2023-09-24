using EmptyBank.Core;
using EmptyBank.MVVM.Model;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyBank.MVVM.ViewModel
{

    internal class SignViewModel: ObservableObject
    {
        public RelayCommand ShowAuthView { get; set; }
        public AuthViewModel AuthViewPage { get; set; }

        private string _textLogin;
        public string TextLogin
        {
            get { return _textLogin; }
            set { _textLogin = value; OnPropertyChanged(); }
        }

        private string _textPass;
        public string TextPass
        {
            get { return _textPass; }
            set { _textPass = value; OnPropertyChanged(); }
        }

        private string _signLoginBrush;
        public string SignLoginBrush
        { 
            get { return _signLoginBrush; } 
            set { _signLoginBrush = value; OnPropertyChanged(); } 
        }

        private string _signPasswordBrush;
        public string SignPasswordBrush
        {
            get { return _signPasswordBrush; }
            set { _signPasswordBrush = value; OnPropertyChanged(); }
        }

        private string _signLoginWarningText;
        public string SignLoginWarningText
        {
            get { return _signLoginWarningText; }
            set { _signLoginWarningText = value; OnPropertyChanged(); }
        }

        private string _signPasswordWarningText;
        public string SignPasswordWarningText
        {
            get { return _signPasswordWarningText; }
            set { _signPasswordWarningText = value; OnPropertyChanged(); }
        }

        public SignViewModel()
        {
            ShowAuthView = new RelayCommand(sender => RegisterButton(sender));
            SignLoginBrush = Brushes.White.ToString();
            SignPasswordBrush = Brushes.White.ToString();
        }

        private void RegisterButton(object sender)
        {
            AuthViewPage = new AuthViewModel();
            PasswordBox password = sender as PasswordBox;

            // вся проблема в TextLogin

            if (password != null) { TextPass = password.Password; }

            if (TextLogin.Length < 3 && TextPass.Length < 3)
            {
                SignLoginWarningText = "Минимальная длина логина - 3 символа.";
                SignPasswordWarningText = "Минимальная длина пароля - 3 символа.";

                SignLoginBrush = Brushes.Red.ToString();
                SignPasswordBrush = Brushes.Red.ToString();
                return;
            }
            else {
                SignLoginWarningText = "";
                SignPasswordWarningText = "";

                SignLoginBrush = Brushes.White.ToString();
                SignPasswordBrush = Brushes.White.ToString();
            }
            MessageBox.Show(TextLogin, TextPass);
            // выводит данные
        }

    }
}
