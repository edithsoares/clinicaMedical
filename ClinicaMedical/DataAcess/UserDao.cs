using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;

namespace DataAcess
{
    public class UserDao: ConnectionToSql
    {
       public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                // Abre o Banco
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "Select * from Users where LoginName=@user and Password=@pass";

                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);

                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CacheDoUsuario.IdUser = reader.GetInt32(0);
                            CacheDoUsuario.LoginName = reader.GetString(1);
                            CacheDoUsuario.Password = reader.GetString(2);
                            CacheDoUsuario.FirstName = reader.GetString(3);
                            CacheDoUsuario.Sobrenome = reader.GetString(4);
                            CacheDoUsuario.Position = reader.GetString(5);
                            CacheDoUsuario.Email = reader.GetString(6);
                        }
                        return true;
                    }else
                    {
                        return false;
                    }

                }
                
            }
        }
    }
}
