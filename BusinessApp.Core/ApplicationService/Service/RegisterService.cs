using System;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.Core.ApplicationService.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        //private readonly ILogger _logger;
        public RegisterService(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            //logger = _logger;
        }
        /*
         *
         * TODO refactor email confirmation
         */
        public async Task<object> Register(User user)
        {
            if (user != null)
            {
                IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(true);
                    await ConfirmEmail(user).ConfigureAwait(true);
                    //await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(true);
                    return result;
                }
                return result;
            }
            return null;
        }

        public async Task<object> ConfirmEmail(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(true);
            await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='code goes here'>clicking here</a>.").ConfigureAwait(true);
            return code;
        }

        public Task<IActionResult> ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> VerifyCode()
        {
            throw new NotImplementedException();
        }
    }
}
