using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAcess
{
    public class ConnectionToSql
    {
        private readonly string connectionString;

        public ConnectionToSql()
        {
            // String de Conexão
            connectionString = @"Server=DESKTOP-AVNQA5V\SQLEXPRESS; DataBase= MyCompany; integrated security= true";
        }

        protected SqlConnection GetConnection()
        {
            // obtem a conexão e a retorna
            return new SqlConnection(connectionString);
        }
    }
}
