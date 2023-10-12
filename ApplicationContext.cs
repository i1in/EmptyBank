using System.Data.SqlClient;

namespace EmptyBank
{
    class DataBase
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
    }
}