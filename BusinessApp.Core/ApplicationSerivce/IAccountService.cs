using BusinessApp.Core.Entitiy.Users;
using System.Threading.Tasks;

namespace BusinessApp.Core.ApplicationSerivce
{
    public interface IAccountService
    {
        Task<object> Login(User user);
        void LogOff();
        Task<object> Register(User user);
        //Task<IActionResult> ResetPassword(User user);
        //Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false);
        //Task<IActionResult> SendCode(SendCodeEntity model);
        //Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null);
        //Task<IActionResult> VerifyCode(VerifyCodeEntity model);
        //Task<IActionResult> ConfirmEmail(string userId, string code);
        //Task<IActionResult> ExternalLoginCallback(string returnUrl = null);
        //Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null);
        //Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model);
    }
}
