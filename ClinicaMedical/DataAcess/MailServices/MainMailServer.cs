using System;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace DataAcess.MailServices
{
    public abstract class MainMailServer
    {
        // Atributos da classe
        private SmtpClient smtpClient;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }


        // inicializar propiedades do Cliente SMTP

        protected void InicializeSmptCliente()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(senderMail, password);
            smtpClient.Host = host;
            smtpClient.EnableSsl = ssl;
        }

        // Enviar mensagems 
        public void sendMail(string subject, string body, List<string> destinatarioMail)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(senderMail);
                foreach (string mail in destinatarioMail)
                {
                    mailMessage.To.Add(mail);
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage); // Envia a mensagem
            }
            catch (Exception) { }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
