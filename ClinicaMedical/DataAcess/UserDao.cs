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

                    command.CommandText = "Select * from Users where UserName=@user and Password=@pass";

                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);

                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        { //UserId, UserName, Password, Nome, Sobrenome, Cargo, Email, Cpf, Telefone 
                            CacheDoUsuario.UserId = reader.GetInt32(0);
                            CacheDoUsuario.UserName = reader.GetString(1);
                            CacheDoUsuario.Password = reader.GetString(2);
                            CacheDoUsuario.Nome = reader.GetString(3);
                            CacheDoUsuario.Sobrenome = reader.GetString(4);
                            CacheDoUsuario.Cargo = reader.GetString(5);
                            CacheDoUsuario.Email = reader.GetString(6);
                            CacheDoUsuario.Cpf = reader.GetString(7);
                            CacheDoUsuario.Telefone = reader.GetString(8);
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
        public bool ExisteUsers(int id, string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    //UserId, UserName, Password, Nome, Sobrenome, Cargo, Email, Cpf, Telefone 
                    command.CommandText = @"select * from Users where UserId=@id and 
                        UserName=@user and password=@pass";

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@user", user);
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
            
                using (var connection =  GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "StMostrar";
                        command.CommandType = CommandType.StoredProcedure;

                        reader = command.ExecuteReader();
                        table.Load(reader);
                        connection.Close();
                        return table;
                    }
                }
           

            
        }

       

        public void Inserir(string userName, string password, string nome, string sobrenome, string cargo, string email, string cpf, string telefone)
        {
            
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    { //UserId, UserName, Password, Nome, Sobrenome, Cargo, Email, Cpf, Telefone 
                        command.Connection = connection;
                        command.CommandText = "StInserir";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@sobrenome", sobrenome);
                        command.Parameters.AddWithValue("@cargo", cargo);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@telefone", telefone);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        connection.Close();
                    }
                }
           
        }

        public void Editar(string userName, string password, string nome, string sobrenome, string cargo, string email, string cpf, string telefone, int userId)
        {
            
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    { //UserId, UserName, Password, Nome, Sobrenome, Cargo, Email, Cpf, Telefone 
                        command.Connection = connection;
                        command.CommandText = "StEditar";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@sobrenome", sobrenome);
                        command.Parameters.AddWithValue("@cargo", cargo);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@cpf", cpf);
                        command.Parameters.AddWithValue("@telefone", telefone);
                        command.Parameters.AddWithValue("@idUser", userId);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        connection.Close();
                    }
                }                      
        }


        public void Excluir(int userId)
        {
           
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "StExcluir";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@userId", userId);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();                        
                        connection.Close();                        
                    }
                }
           
        }
    }
}
