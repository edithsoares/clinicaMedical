using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAcess.MailServices
{
    class SystemSupportMail: MainMailServer
    {
        public SystemSupportMail()
        {
            senderMail = "suportestecod@gmail.com";
            password = "testecod123";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            InicializeSmptCliente();
        }
    }
}
