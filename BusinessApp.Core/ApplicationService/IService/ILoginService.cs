using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusinessApp.Core.ApplicationService.IService
{
    public interface ILoginService
    {
        Task<object> Login(User user);
        void LogOff();

        Task<IActionResult> ResetPassword(User user);
        Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false);
        Task<IActionResult> SendCode();
        Task<IActionResult> ExternalLoginCallback(string returnUrl = null);
        Task<IActionResult> ExternalLoginConfirmation();
    }
}