using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyBank.Core.Service
{
    internal class AuthService
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;

        public void Add(string login, string password)
        {
            try
            {
                connection = new SQLiteConnection("Data Source = bank.db");
                connection.Open();
                command = new SQLiteCommand("INSERT INTO Users (login, pass) VALUES (:login, :pass)", connection);
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("pass", password);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
