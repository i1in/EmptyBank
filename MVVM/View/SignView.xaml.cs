using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.SQLite;

namespace EmptyBank.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для SignView.xaml
    /// </summary>
    public partial class SignView : UserControl
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;

        public void Add(string login, string password)
        {
            // dsfgsdg
            try
            {
                connection = new SQLiteConnection("Data Source = bank.db");
                connection.Open();
                command = new SQLiteCommand("INSERT INTO Users (login, pass) VALUES (:login, :pass)", connection);
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("pass", password);
                command.ExecuteNonQuery();
            } catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public SignView()
        {
            InitializeComponent();
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            string login = SignInBox.Text.Trim();
            string password = SignInPassBox.Password.Trim();
            string repeatPassword = SignInRepeatPassBox.Password.Trim();

            if(login.Length < 3) {
                SignInBox.ToolTip = "Минимальное количество\nсимволов -- 3";
                SignInBox.BorderBrush = Brushes.DarkRed;
                return;
            }
            else { SignInBox.BorderBrush = Brushes.Green; SignInBox.ToolTip = null; }

            if(password.Length < 3) {
                SignInPassBox.ToolTip = "Минимальное количество\nсимволов -- 3";
                SignInPassBox.BorderBrush = Brushes.DarkRed;
                return;
            } 
            else {  SignInPassBox.BorderBrush = Brushes.Green; SignInPassBox.ToolTip = null; }

            if (repeatPassword.Length < 3) {
                SignInRepeatPassBox.ToolTip = "Минимальное количество\nсимволов -- 3";
                SignInRepeatPassBox.BorderBrush = Brushes.DarkRed;
                return;
            } else if (password != repeatPassword) { 
                SignInRepeatPassBox.ToolTip = "Пароли не совпадают"; SignInRepeatPassBox.BorderBrush = Brushes.DarkRed; return;
            }
            else { SignInRepeatPassBox.BorderBrush = Brushes.Green; SignInRepeatPassBox.ToolTip = null; }

            Add(login, password);
            
            
        }
    }
}
