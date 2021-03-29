using SupportApp.Services.EmailService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Services.EmailService
{
    public interface IEmailServiceRepository
    {
        Task<bool> SendConfirmationMail(string inpEmail, string inpBody);
    }
}
