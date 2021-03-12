using System;

using System.Data.SqlClient;

namespace DataAcess
{
    public class ConnectionToSql
    {
        private readonly string connectionString;

        public ConnectionToSql()
        {
            // String de Conexão
            connectionString = @"Server=DESKTOP-AVNQA5V\SQLEXPRESS; DataBase= Clinica; integrated security= true";
        }

        protected SqlConnection GetConnection()
        {
            // obtem a conexão e a retorna
            return new SqlConnection(connectionString);
        }
    }
}
