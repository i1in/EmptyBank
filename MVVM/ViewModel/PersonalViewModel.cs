using EmptyBank.Core;
using EmptyBank.Core.Service;
using EmptyBank.MVVM.Model;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyBank.MVVM.ViewModel
{
    internal class PersonalViewModel : ObservableObject
    {
        public RelayCommand ApproveButton { get; set; }
        public RelayCommand RefreshData { get; set; }
        public RelayCommand SignOut {  get; set; }

        private string _userPasswordBoxDescription = "Рекомендуем периодически\r\nㅤㅤобновлять пароль";
        public string UserPasswordBoxDescription
        {
            get { return _userPasswordBoxDescription; }
            set { _userPasswordBoxDescription = value; OnPropertyChanged(); }
        }

        private string _userPasswordBoxDescriptionColor = Brushes.Gray.ToString();
        public string UserPasswordBoxDescriptionColor
        {
            get { return _userPasswordBoxDescriptionColor; }
            set { _userPasswordBoxDescriptionColor = value; OnPropertyChanged(); }
        }

        private string _userLogin;
        public string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; OnPropertyChanged(); }
        }

        private string _userPassword = "Нынешний пароль";
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; OnPropertyChanged(); }
        }

        private string _newUserPassword;
        public string NewUserPassword
        {
            get { return _newUserPassword; }
            set { _newUserPassword = value; OnPropertyChanged(); }
        }

        private string _userPasswordBrush = Brushes.White.ToString();
        public string UserPasswordBrush
        {
            get { return _userPasswordBrush; }
            set { _userPasswordBrush = value; OnPropertyChanged(); }
        }

        private string _newPasswordBrush = Brushes.White.ToString();
        public string NewPasswordBrush
        {
            get { return _newPasswordBrush; }
            set { _newPasswordBrush = value; OnPropertyChanged(); }
        }

        public PersonalViewModel() 
        { 
            ApproveButton = new RelayCommand(sender => ApproveButtonCallback(sender));
            RefreshData = new RelayCommand(sender => Refresh(sender));

            ServerModel serverModel = new ServerModel();
            UserLogin = serverModel.Login;
        }

        private void Refresh(object sender)
        {
            UserPasswordBoxDescription = "Рекомендуем периодически\r\nㅤㅤобновлять пароль";
            UserPasswordBoxDescriptionColor = Brushes.Gray.ToString();
        }

        private void ApproveButtonCallback(object sender)
        {
            ServerModel model = new ServerModel();
            AuthService AuthService = new AuthService();
            PasswordBox password = sender as PasswordBox;
            if (password != null) { NewUserPassword = password.Password; }

            if (UserPassword == "Нынешний пароль" ||  UserPassword.Length < 3)
            {
                UserPasswordBoxDescription = "Ваш пароль должен начинаться\nㅤㅤㅤот 3 символов.";
                UserPasswordBoxDescriptionColor = Brushes.DarkRed.ToString();
                UserPasswordBrush = Brushes.DarkRed.ToString();
                return;
            } else { UserPasswordBrush = Brushes.White.ToString(); }

            if(NewUserPassword == "Новый пароль" ||  NewUserPassword.Length == 0)
            {
                UserPasswordBoxDescription = "Новый пароль должен начинаться\nㅤㅤㅤㅤот 3 символов.";
                UserPasswordBoxDescriptionColor = Brushes.DarkRed.ToString();
                NewPasswordBrush = Brushes.DarkRed.ToString();
                return;
            } else { NewPasswordBrush = Brushes.White.ToString(); }

            if (UserPassword != model.Password)
            {
                UserPasswordBoxDescription = "Введенный пароль отличается\n от нынешнего.";
                UserPasswordBoxDescriptionColor = Brushes.DarkRed.ToString();
                UserPasswordBrush = Brushes.DarkRed.ToString();
                return;
            } else { UserPasswordBrush= Brushes.White.ToString(); }

            if (NewUserPassword == model.Password)
            {
                UserPasswordBoxDescription = "Новый пароль ничем не\nотличается от нынешнего.";
                UserPasswordBoxDescriptionColor = Brushes.DarkRed.ToString();
                NewPasswordBrush = Brushes.DarkRed.ToString();
                return;
            } else { 
                NewPasswordBrush = Brushes.White.ToString(); 
            }
            AuthService.UpdatePassword(NewUserPassword);
            UserPasswordBoxDescription = "Вы успешно сменили пароль.\nОбновите страницу.";
            UserPasswordBoxDescriptionColor = Brushes.Green.ToString();
            UserPassword = string.Empty;
            NewUserPassword = string.Empty;
        }
    }
}
