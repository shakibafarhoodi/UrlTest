using Domin.Model;
using System.Net;
using System.Net.Mail;

namespace App.Services
{
    public class IEmailSenderServices : IEmailSender
    {
        public async Task SendEmailAsync(EmailModelViewModel email)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress("pnsxfp@gmail.com", "PNSIT"),
                To = { email.To },
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true

            };
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("pnsxfp@gmail.com", "uoog aewz guxd nkwc"),
                EnableSsl = true

            };
            smtpClient.Send(message);
            await Task.CompletedTask;

            //Not-Envrypted 25 ,
            //secure tls 587,
            //secure ssl 565,

        }
    }
}

