using Domin.Model;

namespace App.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModelViewModel email);

    }
}
