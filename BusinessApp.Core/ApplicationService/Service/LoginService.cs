using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.Core.ApplicationService.Service
{
    public class LoginService : ILoginService
    {
        //private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenManagerService _tokenManagerService;
        //private readonly ILogger _logger;
        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager,ITokenManagerService tokenManagerService)
        {
            //_userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManagerService = tokenManagerService;
            //logger = _logger;
        }

        public async Task<object> Login(User user)
        {
            User signedUser = await _userManager.FindByEmailAsync(user.Email).ConfigureAwait(true);
            if (signedUser != null)
            {
                var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, user.PasswordHash, isPersistent: false, lockoutOnFailure: false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    var token = _tokenManagerService.GenerateJwtToken(signedUser);
                    return new { token = token};
                }
                 return result; 
            }
            return ("ops something went wrong"); 
        }

        public async void LogOff()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(true);
        }

        public Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ExternalLoginConfirmation()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ResetPassword(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SendCode()
        {
            throw new NotImplementedException();
        }
    }
}
