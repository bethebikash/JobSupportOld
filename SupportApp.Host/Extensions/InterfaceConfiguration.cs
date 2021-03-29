using Microsoft.Extensions.DependencyInjection;
using SupportApp.Authorization.Account;
using SupportApp.Core.Mentors;
using SupportApp.Services.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportApp.Host.Extensions
{
    public static class InterfaceConfiguration
    {
        public static void AddInterfaceConfig(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IEmailServiceRepository, EmailServiceRepository>();
            services.AddTransient<IMentorsRepository, MentorsRepository>();
        }
    }
}
