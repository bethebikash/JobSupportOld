using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Services.EmailService.Dto
{
    public class ConfirmationMailDto
    {
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
