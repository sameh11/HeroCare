using BusinessApp.Core.ApplicationSerivce;
using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entitiy.Users;
using HeroCare.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeroCare.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager
            , IAccountService accountService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<object> Login(LoginDto loginDto)
        {
            var user = new User { Email = loginDto.Email, PasswordHash = loginDto.Password };
            if (ModelState.IsValid)
            {
                await _accountService.Login(user).ConfigureAwait(true);
            }
            // If we got this far, something failed, redisplay form
            return BadRequest("hell now something went wrong"); ;
        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //requires error handling 
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            var user = new User { Email = registerDto.Email, PasswordHash = registerDto.Password, UserName = registerDto.UserName };
            if (ModelState.IsValid)
            {
                await _accountService.Register(user).ConfigureAwait(true);
            }
            // If we got this far, something failed, redisplay form
            return BadRequest("something went wrong");
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            _accountService.LogOff();
            return Ok();
        }


    }
}