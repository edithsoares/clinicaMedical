using System;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;

namespace DataAcess
{
    //
    // - Camada de acesso a dados
    //

    public class UserDao: ConnectionToSql
    {
        // Login
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


        //
        // - Aplicar segurança contra ataques
        //

        // Verificação de Usúarios
        public bool ExisteUsers(int id, string loginName, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using(var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = @"select * from users where userId=@id and 
                        loginName=@loginName and password=@pass";

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@loginName", loginName);
                    command.Parameters.AddWithValue("@pass", pass);

                    command.CommandType = CommandType.Text;

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }

        //
        // - Segurança de acordo com os tipos ou posições / Cargos dos usuários.
        //

        // Verifica Posição / Cargo de user
        public void ManagePermissions()
        {
            if (CacheDoUsuario.Position == Positions.Receptionist)
            {
                // Add métodos de restrição de acesso ao user.
            }
            else if (CacheDoUsuario.Position == Positions.Accounting)
            {
                // Add métodos de restrição de acesso ao user.
            }
            else if (CacheDoUsuario.Position == Positions.Administrator)
            {
                // Add métodos de restrição de acesso ao user.
            }
            else
            {
                // User não encontrado ou Carho não existe
            }
        }
    }   
}
