using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportApp.Authorization.Account;
using SupportApp.Authorization.Account.Dto;
using SupportApp.Core.Helper;
using SupportApp.Core.Mentors;
using SupportApp.Services.EmailService;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Application.v1.Accounts
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountsController: ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailServiceRepository _emailRepository;
        private readonly IMentorsRepository _mentorReposirtory;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountsController(IAccountRepository accountRepository, UserManager<IdentityUser> userManager,
            IEmailServiceRepository emailRepository, IMentorsRepository mentorReposirtory,
            SignInManager<IdentityUser> signInManager
            )
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _emailRepository = emailRepository;
            _mentorReposirtory = mentorReposirtory;
            _signInManager = signInManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MentorSignUp(CreateAccountDto input)
        {
            var validationResult = await _accountRepository.ValidateSignUp(input);
            if(validationResult.Success == false)
            {
                return BadRequest(validationResult);
            }
            var newAccount = new IdentityUser()
            {
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber
            };
            var createAccount = await _userManager.CreateAsync(newAccount, input.Password);
            if (createAccount.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newAccount);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Accounts", new { userId = newAccount.Id, token = token }, Request.Scheme);
                bool emailResponse = await _emailRepository.SendConfirmationMail(newAccount.Email, confirmationLink);

                if (emailResponse == true)
                {
                    var user = await _accountRepository.CreateUser(newAccount);
                    var mentor = await _mentorReposirtory.CreateMentor(user);
                    await _userManager.AddToRoleAsync(newAccount, "MENTOR");
                    ResultResponse response = new ResultResponse()
                    {
                        Success = true,
                        Message = "Registeration successful. Please check your email for confirmation link !"
                    };
                    return Ok(response);
                }
                else
                {
                    await _userManager.DeleteAsync(newAccount);
                    ResultResponse response = new ResultResponse()
                    {
                        Success = false,
                        Message = "Something went wrong. Please try again !"
                    };
                    return BadRequest(response);
                }
            }
            else
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Something went wrong. Please try again !"
                };
                return BadRequest(response);
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var confirm = await _userManager.ConfirmEmailAsync(user, token);
            if (confirm.Succeeded)
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = true,
                    Message = "Email confirmation successful !"
                };
                return Ok(response);
            }
            else
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Confirmation failed !"
                };
                return BadRequest(response);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInDto input)
        {
            var account = await _userManager.FindByNameAsync(input.UserName);
            if(account == null)
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Username and password don't match !"
                };
                return BadRequest(response);
            }
            if(account.EmailConfirmed == false)
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Please verify your email before signing in !"
                };
                return BadRequest(response);
            }
            var signIn = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, input.RememberMe, lockoutOnFailure: true);
            if (signIn.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(input.UserName);
                var token = new
                {
                    UserId = user.Id,
                    Token = await _accountRepository.GenerateToken(user)
                };
                ResultResponse response = new ResultResponse()
                {
                    Success = true,
                    Message = "Welcome !",
                    Data = token
                };
                return Ok(response);
            }
            else
            {
                ResultResponse response = new ResultResponse()
                {
                    Success = false,
                    Message = "Username and password don't match !"
                };
                return BadRequest(response);
            }

        }
    }
}
