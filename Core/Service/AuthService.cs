using System;
using System.Data.SQLite;
using System.Linq;
using System.Windows;

namespace EmptyBank.Core.Service
{
    internal class AuthService
    {
        static SQLiteConnection connection;
        static SQLiteCommand command;
        ApplicationContext db;

        public bool IsExists(string login)
        {
            User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.FirstOrDefault(x => x.Login == login);
                if (user != null) { return true; } else { return false; }
            }
        }

        public bool Find(string login, string password)
        {
            User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.FirstOrDefault(x => x.Login == login && x.Pass == password);
                if (user != null) { return true; } else { return false; }
            }
        }

        public void Add(string login, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source = bank.db"))
                {
                    connection.Open();

                    var command = new SQLiteCommand("INSERT INTO Users (login, pass) VALUES (:login, :pass)", connection);
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("pass", password);
                    command.ExecuteNonQuery();
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public void Remove(string login, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source = bank.db"))
                {
                    connection.Open();

                    var command = new SQLiteCommand("DELETE FROM Users WHERE login=@login AND pass=@pass", connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@pass", password);
                    command.ExecuteNonQuery();
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public void Show()
        {
            try
            {
                db = new ApplicationContext();
                connection = new SQLiteConnection("Data Source = bank.db");
                connection.Open();
                command = new SQLiteCommand("SELECT * FROM Users", connection);
                string str = "";
                foreach(User user in db.Users.ToList() )
                {
                    str += $"{user.id}: {user.Login}_{user.Pass}\n";
                }
                MessageBox.Show(str);

                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        public void DropTable()
        {
            connection = new SQLiteConnection("Data Source = bank.db");
            connection.Open();
            command = new SQLiteCommand("DROP TABLE Users", connection);
            command.ExecuteNonQuery();
            return;
        }
    }
}
