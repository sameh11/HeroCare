using System.Threading.Tasks;
using BusinessApp.Core.ApplicationService;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.Entity.Users;
using HeroCare.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroCare.Controllers.Account
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController: ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegistrationController(IRegisterService registerService)
        {
            _registerService = registerService;
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
                return res; 
            }
            // If we got this far, something failed, redisplay form
            return BadRequest("something went wrong");
        }

       
        //Task<IActionResult> ResetPassword(User user);
        //Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model);

    }
}
