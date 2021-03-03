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

        SqlDataReader reader = null;
        DataTable table = new DataTable();
     

        // Validar o nome de usuário e senha para o log
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
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

                using (var command = new SqlCommand())
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

        // OPERAÇÕES DO CRUD
           

        public DataTable Exibir()
        {
            try
            {
                using (var connection =  GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "MostrarUsers";
                        command.CommandType = CommandType.StoredProcedure;

                        reader = command.ExecuteReader();
                        table.Load(reader);
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception)
            {
               
                throw;
            }

            
        }

       

        public void Inserir(string loginName, string password, string firstName, string sobrenome, string position, string email)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "InserirUsers";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@loginName", loginName);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@sobrenome", sobrenome);
                        command.Parameters.AddWithValue("@position", position);
                        command.Parameters.AddWithValue("@email", email);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Editar(string loginName, string password, string firstName, string sobrenome, string position, string email, int idUser)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "EditarUser";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@loginName", loginName);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@sobrenome", sobrenome);
                        command.Parameters.AddWithValue("@position", position);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@idUser", idUser);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Excluir(int idUser)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "ExcluirUser";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@idUser", idUser);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();                        
                        connection.Close();                        
                    }
                }
            }
            catch (Exception)
            {
              
                throw;
            }
        }
    }
}
