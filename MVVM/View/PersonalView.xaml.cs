using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmptyBank.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для PersonalView.xaml
    /// </summary>
    public partial class PersonalView : UserControl
    {
        public PersonalView()
        {
            InitializeComponent();
        }

        private void UserPasswordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Нынешний пароль") (sender as TextBox).Text = string.Empty;
        }

        private void UserPasswordBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Нынешний пароль" || (sender as TextBox).Text.Length == 0) (sender as TextBox).Text = "Нынешний пароль";
        }

        private void UserPasswordChanged(object sender, RoutedEventArgs e)
        {
            FakePasswordBox.Text = UserPasswordBox.Password;
        }

        void ShowFakePasswordBox()
        {
            FakePasswordBox.Visibility = Visibility.Visible;
            UserPasswordBox.Visibility = Visibility.Hidden;
            FakePasswordBox.Text = UserPasswordBox.Password;
        }
        
        void HidePassword()
        {
            FakePasswordBox.Visibility = Visibility.Hidden;
            UserPasswordBox.Visibility = Visibility.Visible;
            if (FakePasswordBox.Text != "Новый пароль")
                UserPasswordBox.Password = FakePasswordBox.Text;
            else
                UserPasswordBox.Password = "";

        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            UserPasswordBox.Password = FakePasswordBox.Text;
            ShowFakePasswordBox();
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            UserPasswordBox.Password = FakePasswordBox.Text;
            HidePassword();
        }

        private void UserPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserPasswordBox.Password.Length == 0) FakePasswordBox.Text = "Новый пароль";
        }

        private void UserPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(UserPasswordBox.Password == "Новый пароль" || FakePasswordBox.Text == "Новый пароль")
            {
                FakePasswordBox.Visibility = Visibility.Hidden;
                UserPasswordBox.Password = string.Empty;
            }
            FakePasswordBox.Visibility = Visibility.Hidden;
        }

        private void FakePasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FakePasswordBox.Text == "Новый пароль" || UserPasswordBox.Password == "Новый пароль") FakePasswordBox.Text = string.Empty;
            UserPasswordBox.Visibility = Visibility.Hidden;
        }

        private void FakePasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(FakePasswordBox.IsFocused) UserPasswordBox.Password = FakePasswordBox.Text;
        }

        private void FakePasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserPasswordBox.Password.Length == 0) FakePasswordBox.Text = "Новый пароль";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsRemember = false;
            Properties.Settings.Default.userID = 0;
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
        }
    }
}
