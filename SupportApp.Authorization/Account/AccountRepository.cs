using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupportApp.Authorization.Account.Dto;
using SupportApp.Core.Helper;
using SupportApp.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Authorization.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<User> CreateUser(IdentityUser input)
        {
                var newUser = new User()
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    PhoneNumber = input.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    ApplicationUserId = input.Id
                };
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();
            return newUser;
              
        }
        public async Task<List<GetAllAccountsDto>> GetAllAccounts()
        {
            List<GetAllAccountsDto> accounts = await _dbContext.Users.Select(x => new GetAllAccountsDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToListAsync();
            return accounts;
        }

        public async Task<IList<string>> GetRoles(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<string> GenerateToken(IdentityUser user)
        {
            var roles = await GetRoles(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user", user.Id)
            };
            foreach (var userRole in roles)
            {
                claims.Add(new Claim("roles", userRole));
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<ResultResponse> ValidateSignUp(CreateAccountDto input)
        {
            if (input.UserName == null || input.Email == null || input.PhoneNumber == null || input.Password == null)
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Fields cannot be empty !"
                };
                return response;
            }
            if (input.Password.Length < 8)
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Password must be at least 8 characters long ! "
                };
                return response;
            }
            var accounts = await GetAllAccounts();
            if (accounts.Select(x => x.Email).Contains(input.Email) || accounts.Select(x => x.PhoneNumber).Contains(input.PhoneNumber))
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "User already exists !"
                };
                return response;
            }
            if (accounts.Select(x => x.UserName).Contains(input.UserName))
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Username already taken !"
                };
                return response;
            }
            ResultResponse respoonse = new ResultResponse()
            {
                Success = true
            };
            return respoonse;
        }
    }
}
