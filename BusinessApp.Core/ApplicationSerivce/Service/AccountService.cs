//using BusinessApp.Core.DomainService.UserRepository;
using BusinessApp.Core.Entitiy.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BusinessApp.Core.ApplicationSerivce.Service
{
    public class AccountService : IAccountService
    {
        //private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly ILogger _logger;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            //_userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            //logger = _logger;
        }

        public async Task<object> Login(User user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.PasswordHash, user.RememberMe, lockoutOnFailure: false).ConfigureAwait(true);
            if (result.Succeeded)
            {
                //_logger.LogInformation(1, "User logged in.");
                return result;
            }
            else
            {
                //TODO consider refactoring 
                IdentityError err = new IdentityError
                {
                    Code = "validation",
                    Description = "Invalid username or password"
                };
                //var i = (object)result;
                return System.Tuple.Create<object, object>(err, (object)result);
                //return true;
            }
        }


        public async Task<object> Register(User user)
        {
            var result = new IdentityResult();
            if (user != null)
            {
                var _user = new User { UserName = user.Email, Email = user.Email, PasswordHash = user.PasswordHash };
                result = await _userManager.CreateAsync(_user, _user.PasswordHash).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(true);
                    return result;
                }
            }
            
            return result.Errors;
        }

        public async void LogOff()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(true);
        }


    }
}
