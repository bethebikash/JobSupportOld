using Microsoft.AspNetCore.Identity;
using SupportApp.Authorization.Account.Dto;
using SupportApp.Core.Helper;
using SupportApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Authorization.Account
{
    public interface IAccountRepository
    {
        Task<User> CreateUser(IdentityUser input);
        Task<List<GetAllAccountsDto>> GetAllAccounts();
        Task<IList<string>> GetRoles(IdentityUser input);
        Task<string> GenerateToken(IdentityUser input);
        Task<ResultResponse> ValidateSignUp(CreateAccountDto input);
    }
}
