using System;
using System.Collections.Generic;
using System.Text;

namespace SupportApp.Authorization.Account.Dto
{
    public class SignInDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
