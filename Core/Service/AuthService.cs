using System;
using EmptyBank.MVVM.Model;
using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Globalization;
using EmptyBank.MVVM.ViewModel;
using System.Data.Entity;

namespace EmptyBank.Core.Service
{
    internal class AuthService
    {
        SqlConnection sqlConnection = new SqlConnection(@"
            Data Source = NOT1LIN\SQLEXPRESS; 
            Initial Catalog = bank;
            Integrated Security = True");

        ServerModel ServerModel { get; set; }

        public void Connect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) sqlConnection.Open();
        }

        public void Disconnect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) sqlConnection.Close();
        }

        public SqlConnection Connection() { return sqlConnection; }

        public void Add(string username, string password)
        {
            DataBase database = new DataBase();
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            database.Connect();

            Random rand = new Random();
            string genCard = $"22007569{rand.Next(10000001, 99999999)}";
            SqlCommand cmd = new SqlCommand($"SELECT card_number FROM Users WHERE card_number='{genCard}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1) genCard = $"22007569{rand.Next(10000001, 99999999)}";


            SqlCommand command = new SqlCommand($"insert into Users (login, password, balance, bonuses, limit, card_number, cvc, no_commission) " +
                $"values ('{username}', '{password}', {rand.Next(10001, 99999)}, 0, 1000000, '{genCard}', {rand.Next(101, 999)}, 50000)", database.Connection());

            command.ExecuteNonQuery();
            database.Disconnect();
            MessageBox.Show("Успешно!", "EmptyBank", MessageBoxButton.OK);
            return;
        }

        public void Remove(string username, string password)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlCommand cmd = new SqlCommand($"DELETE FROM Users WHERE login={username} AND password={password}",database.Connection());
            cmd.ExecuteNonQuery();
            database.Disconnect();
            MessageBox.Show($"Пользователь {username} был успешно удалён из базы.");
            return;
        }

        public bool FindUserID(string username, string password)
        {
            DataBase database = new DataBase();
            ServerModel serverModel = new ServerModel();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT id, login, password FROM Users WHERE login='{username}' AND password='{password}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    serverModel.Id = Convert.ToInt32(row["id"]);
                }

                return true;
            }
            else return false;
        }

        public string GetID(string login, string password)
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE login='{login}' AND password='{password}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            string result = "";
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    result = row["id"].ToString();
                }
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public void FindByID(int id)
        {
            DataBase database = new DataBase();
            ServerModel model = new ServerModel();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Users WHERE id='{id}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    model.Login = row["login"].ToString();
                    model.Password = row["password"].ToString();
                    model.Balance = Convert.ToDouble(row["balance"]);
                    model.Bonuses = Convert.ToDouble(row["bonuses"]);
                    model.Limit = Convert.ToDouble(row["limit"]);
                    model.CardNumber = Convert.ToInt64(row["card_number"]);
                    model.Cvc = Convert.ToInt32(row["cvc"]);
                    model.NoCommission = Convert.ToDouble(row["no_commission"]);
                }
            }
        }

        public void UpdatePassword(string password)
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();
            string bufferpass = serverModel.Password;

            SqlCommand cmd = new SqlCommand($"UPDATE Users SET password='{password}' WHERE id='{serverModel.Id}'", database.Connection());
            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    serverModel.Password = row["login"].ToString();
                }
            }
            database.Disconnect();
            return;
        }

        public void CardReceiver(string card)
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT id, login, balance, card_number FROM Users WHERE card_number='{card}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    serverModel.ReceiverNickname = row["login"].ToString();
                    serverModel.ReceiverBalance = float.Parse(row["balance"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    serverModel.ReceiverId = Convert.ToInt32(row["id"]);
                }
            }
        }

        public void CardReceiverByLogin(string login)
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT login, balance, card_number FROM Users WHERE login='{login}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    serverModel.ReceiverNickname = row["login"].ToString();
                    serverModel.ReceiverBalance = float.Parse(row["balance"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    serverModel.ReceiverCard = Convert.ToInt64(row["card_number"]);
                }
            }
        }

        public void UpdateData(string senderCard, string receiverCard, string senderBalance, string receiverbalance, string limit, string cashback, string commission)
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"UPDATE Users SET balance=@balance, limit=@limit, bonuses=@bonuses, no_commission=@comm WHERE login='{serverModel.Login}' AND card_number='{senderCard}'", database.Connection());
            cmd.Parameters.AddWithValue("@balance", Convert.ToDouble(senderBalance));
            cmd.Parameters.AddWithValue("@limit", Convert.ToDouble(limit));
            cmd.Parameters.AddWithValue("@bonuses", Convert.ToDouble(cashback));
            cmd.Parameters.AddWithValue("@comm", Convert.ToDouble(commission));
            cmd.ExecuteNonQuery();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    serverModel.Balance = Convert.ToSingle(row["balance"].ToString());
                    serverModel.Bonuses = (int)Convert.ToDouble(row["bonuses"].ToString());
                }
            }

            SqlCommand receiverCmd = new SqlCommand($"UPDATE Users SET balance=@balance WHERE login='{serverModel.ReceiverNickname}' AND card_number='{receiverCard}'", database.Connection());
            receiverCmd.Parameters.AddWithValue("@balance", Convert.ToDouble(receiverbalance));
            receiverCmd.ExecuteNonQuery();
        }

        public bool UsernameExists(string login)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT login FROM Users WHERE login='{login}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1) return true; else return false;
        }

        public bool CardExists(string card)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT card_number FROM Users WHERE card_number='{card}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1) return true; else return false;
        }

        public void AddToLog(string id, string action, string receiver, string balance, string datetime)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlCommand cmd = new SqlCommand($"insert into UserLog (sender, action, receiver, summary, datetime) " +
                $"values ('{id}', '{action}', '{receiver}', '{balance}', '{datetime}')", database.Connection());

            cmd.ExecuteNonQuery();
            database.Disconnect();
            return;
        }

        public string GetBalance() 
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            string result = "";
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    result = row["balance"].ToString();
                }
                return result;
            } 
            else
            {
                return string.Empty;
            }
        }
        
        public string GetCashback() 
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            string result = "";
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    result = row["bonuses"].ToString();
                }
                return result;
            } 
            else
            {
                return string.Empty;
            }
        }

        public string GetLimit()
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            string result = "";
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    result = row["limit"].ToString();
                }
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetCommission()
        {
            ServerModel serverModel = new ServerModel();
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE id='{serverModel.Id}'", database.Connection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            string result = "";
            if (dataTable.Rows.Count == 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    result = row["no_commission"].ToString();
                }
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public bool Find(string name, string password)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT id, login, password FROM Users WHERE login='{name}' AND password='{password}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if(dataTable.Rows.Count == 1 ) return true; else return false;
        }

        public bool IsExists(string name)
        {
            DataBase database = new DataBase();
            database.Connect();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand($"SELECT id, login, password FROM Users WHERE login='{name}'", database.Connection());

            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1) return true; else return false;
        }
    }
}
