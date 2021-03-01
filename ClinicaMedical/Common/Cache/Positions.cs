using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    //
    // - Segurança de acordo com os tipos ou posições dos usuários.
    //

    public struct Positions
    {   
        // Armazenar as posições do usuário
        

        public const string Administrator = "Administrator";
        public const string Receptionist = "Receptionist";
        public const string Accounting = "Accounting";
    }
}
