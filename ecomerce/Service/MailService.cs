using System.Net.Mail;
using System.Net;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using ecomerce.IService;

namespace ecomerce.Service
{
    public class MailService:IMailKitService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "ducvnhe163208@fpt.edu.vn";
            var pw = "hmyugpbljshgfgwd";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            return client.SendMailAsync(
                new MailMessage(from: mail, to: email, subject, message));
        }
    }
}