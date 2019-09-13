using BusinessApp.Core.ApplicationService;
using BusinessApp.Core.Entity.Users;
using HeroCare.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessApp.Core.ApplicationService.IService;
using BusinessApp.Core.ApplicationService.Service;

namespace HeroCare.Controllers.Account
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService accountService)
        {
            _loginService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = new User { Email = loginDto.Email, PasswordHash = loginDto.Password };
            if (ModelState.IsValid)
            {
                await _loginService.Login(user).ConfigureAwait(true);
            }
            return BadRequest("hell now something went wrong");
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            _loginService.LogOff();
            return Ok();
        }
    }
}