using System;
using DataAcess;
using Common.Cache;
using System.Data;

namespace Domain
{

    //
    // - Camada de Domínio / Negócios
    //

    public class UserModel
    {
        UserDao userDao = new UserDao();
        
 
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }

        //
        // Segurança contra ataques
        //

        public bool SecurityLogin()
        {
            // Se user 
            if (CacheDoUsuario.UserId >= 1)
            {
                // Verifica se os dados estão no banco
                if (userDao.ExisteUsers(CacheDoUsuario.UserId, CacheDoUsuario.UserName, CacheDoUsuario.Password) == true)
                    return true;
                else
                    return false;
            }// Se user vazio
            else
                return false;
        }

       // Métodos do Crud de configuração
       public DataTable Mostrar()
        {
            _ = new DataTable();
            DataTable table = userDao.Exibir();
            return table;
        }

        public void Inserir(string loginName, string password, string firstName, string sobrenome, string position, string email, string cpf, string telefone)
        {
            userDao.Inserir(loginName, password, firstName, sobrenome, position, email, cpf, telefone);
        }

        public void Editar(string userName, string password, string nome, string sobrenome, string cargo, string email,string cpf, string telefone, string userId)
        {
            userDao.Editar(userName, password, nome, sobrenome, cargo, email, cpf, telefone, Convert.ToInt32(userId));
        }

        public void Excluir(int userId)
        {
            userDao.Excluir(userId);
        }

        // Recuperar senha
        public string RecoverPassword(string userRequesting)
        {
            return userDao.RecoverPassword(userRequesting);
        }
    }
}
