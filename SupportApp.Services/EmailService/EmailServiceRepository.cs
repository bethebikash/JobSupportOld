using Microsoft.Extensions.Options;
using SupportApp.Core.Helper;
using SupportApp.Services.EmailService.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Services.EmailService
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        public EmailServiceRepository(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public async Task<bool> SendConfirmationMail(string inpEmail, string inpBody)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(_emailSettings.Value.From),
            };
            mailMessage.To.Add(new MailAddress(inpEmail));
            mailMessage.Subject = _emailSettings.Value.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = inpBody;

            using(SmtpClient client = new SmtpClient())
            {
                NetworkCredential credentials = new NetworkCredential("gyanutest@gmail.com", "Iamnumber4");
                client.Credentials = credentials;
                client.Host = _emailSettings.Value.Host;
                client.Port = _emailSettings.Value.Port;
                client.EnableSsl = true;

                try
                {
                    client.Send(mailMessage);
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
    }
}
