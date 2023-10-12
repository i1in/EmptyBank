using EmptyBank.Core;
using EmptyBank.Core.Service;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyBank.MVVM.ViewModel
{

    internal class SignViewModel: ObservableObject
    {
        public RelayCommand ShowAuthView { get; set; }
        public RelayCommand ShowAuthViewMini { get; set; }
        public AuthViewModel AuthViewPage { get; set; }

        private string _textLogin = string.Empty;
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

        private string _signTextBlock;
        public string SignTextBlock
        {
            get { return _signTextBlock; }
            set { _signTextBlock = value; OnPropertyChanged(); }
        }

        private string _signTextBlockMini;
        public string SignTextBlockMini
        {
            get { return _signTextBlockMini; }
            set { _signTextBlockMini = value; OnPropertyChanged(); }
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

        private bool _signButtonEnabled;
        public bool SignButtonEnabled
        {
            get { return _signButtonEnabled; }
            set { _signButtonEnabled = value; OnPropertyChanged(); }
        }

        public SignViewModel()
        {
            ShowAuthView = new RelayCommand(sender => RegisterButton(sender));
            ShowAuthViewMini = new RelayCommand(sender => NavigateToAuth(sender));

            SignLoginBrush = Brushes.White.ToString();
            SignPasswordBrush = Brushes.White.ToString();

            SignTextBlock = "Регистрация";
            SignTextBlockMini = "Создание учётной записи";
            SignButtonEnabled = true;
        }

        private void RegisterButton(object sender)
        {
            AuthViewPage = new AuthViewModel();
            AuthService authService = new AuthService();
            PasswordBox password = sender as PasswordBox;

            if (password != null) { TextPass = password.Password; }

            if (TextLogin.Length < 3 || TextLogin.Length > 16)
            {
                SignLoginWarningText = "Длина логина должна составлять от 3 до 16 символов";
                SignLoginBrush = Brushes.Red.ToString();
                return;
            }
            else { SignLoginWarningText = ""; SignLoginBrush = Brushes.Green.ToString(); }

            if (TextPass.Length < 3 || TextPass.Length > 16)
            {
                SignPasswordWarningText = "Длина пароля должна составлять от 3 до 16 символов";
                SignPasswordBrush = Brushes.Red.ToString();
                return;
            }
            else { SignPasswordWarningText = ""; SignPasswordBrush = Brushes.Green.ToString(); }

            if (authService.IsExists(TextLogin))
            {
                SignLoginWarningText = "Данный пользователь уже существует";
                SignLoginBrush = Brushes.Red.ToString();
                return;
            } else { SignLoginWarningText = ""; SignLoginBrush = Brushes.Green.ToString(); }

            SignTextBlock = "Успешно!";
            SignTextBlockMini = "Вы зарегистрированы. Перенаправляем...";
            SignButtonEnabled = false;

            authService.Add(TextLogin, TextPass);
            MainViewModel.Instance.CurrentView = AuthViewPage;
            return;
        }

        private void NavigateToAuth(object sender)
        {
            MainViewModel.Instance.CurrentView = new AuthViewModel();
            return;
        }

    }
}
