using System;
using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;

namespace EmptyBank.Core.Service
{
    internal class AuthService
    {
        SqlConnection sqlConnection = new SqlConnection(@"
            Data Source = NOT1LIN\SQLEXPRESS; 
            Initial Catalog = bank;
            Integrated Security = True");

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
            database.Connect();

            Random rand = new Random();

            SqlCommand cmd = new SqlCommand($"insert into Users (login, password, balance, bonuses, limit, card_number, cvc, no_commission) " +
                $"values ('{username}', '{password}', 50000, 0, 1000000, {rand.Next(100001, 999999)}, {rand.Next(101, 999)}, 50000)", database.Connection());

            cmd.ExecuteNonQuery();
            database.Disconnect();
            MessageBox.Show($"Пользователь {username} был успешно занесён в базу.");
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
