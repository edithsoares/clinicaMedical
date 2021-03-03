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
            if (CacheDoUsuario.IdUser >= 1)
            {
                // Verifica se os dados estão no banco
                if (userDao.ExisteUsers(CacheDoUsuario.IdUser, CacheDoUsuario.LoginName, CacheDoUsuario.Password) == true)
                    return true;
                else
                    return false;
            }// Se user vazio
            else
                return false;
        }

       // Métodos do Crud de configuração
       public DataTable MostrarUsers()
        {
            _ = new DataTable();
            DataTable table = userDao.Exibir();
            return table;
        }

        public void InserirUsers(string loginName, string password, string firstName, string sobrenome, string position, string email)
        {
            userDao.Inserir(loginName, password, firstName, sobrenome, position, email);
        }

        public void EditarUser(string loginName, string password, string firstName, string sobrenome, string position, string email, string idUser)
        {
            userDao.Editar(loginName, password, firstName, sobrenome, position, email, Convert.ToInt32(idUser));
        }

        public void ExcluirUser(int idUser)
        {
            userDao.Excluir(idUser);
        }



    }
}
