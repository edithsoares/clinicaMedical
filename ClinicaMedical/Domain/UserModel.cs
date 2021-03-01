using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Common.Cache;

namespace Domain
{

    //
    // - Camada de Domínio / Negócios
    //

    public class UserModel
    {
        UserDao userDao = new UserDao();

        // Login
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
