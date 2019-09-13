using System;
using System.Threading.Tasks;
using System.Web;
using BusinessApp.Core.ApplicationService;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entity.Users;
using HeroCare.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HeroCare.Controllers.Account
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController: ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public RegistrationController(IRegisterService registerService, IUserRepository userRepository, UserManager<User> userManager)
        {
            _registerService = registerService;
            _userRepository = userRepository;
            _userManager = userManager; 
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //requires error handling 
        public async Task<object> Registration([FromBody]RegisterDto registerDto)
        {

            var user = new User { UserName = registerDto.UserName, Email = registerDto.Email, PasswordHash = registerDto.Password };
            if (ModelState.IsValid)
            {
                object res = await _registerService.Register(user).ConfigureAwait(true);
                return  res; 
            }
            return BadRequest("something went wrong");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> ConfirmEmail(string Id= "",string confirmCode = "")
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(confirmCode))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }
            return await _registerService.ConfirmEmail(Id, confirmCode).ConfigureAwait(true);
        }
        //Task<IActionResult> ResetPassword(User user);
        //Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model);

    }
}
