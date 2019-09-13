using System;
//using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.Core.ApplicationService.Service
{
    public class RegisterService : ControllerBase,IRegisterService
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
        
        public async Task<object> Register(User user)
        {
            if (user != null)
            {
                IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    string code =  HttpUtility.UrlEncode(await GenerateCode(user).ConfigureAwait(true));
                    string callbackUrl = "https://localhost:5001/api/Registration/ConfirmEmail?Id=" + user.Id + "&confirmCode=" + code;
                    await _emailService.SendEmailAsync(user.Email, "Confirm your email from the link below" ,
                        $"Please confirm your account by <a href='{callbackUrl}'>{callbackUrl}</a>clicking here.")
                        .ConfigureAwait(true);
                    //TODO consider commenting this line according to the use case
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(true);
                    return result;
                }
                return result;
            }
            return null;
        }

        public async Task<string> GenerateCode(User user)
        {
            var confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(true);
            return confirmCode;
        }

        public async Task<object> ConfirmEmail(string userId, string token)
        {
            if (userId == null)
            {
                return ("Error");
            }
            var getUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if (getUser == null)
            {
                return ("user not found ");
            }
            //the confirmEmailAsync() Is decoding the token automatically 
            var result = await _userManager.ConfirmEmailAsync(getUser, token).ConfigureAwait(true);
            return result;
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
