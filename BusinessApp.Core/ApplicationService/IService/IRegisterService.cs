﻿using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusinessApp.Core.ApplicationService.IService
{
    public interface IRegisterService
    {
        Task<object> Register(User user);
        Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null);
        Task<IActionResult> VerifyCode();
        Task<object> ConfirmEmail(User user);
        Task<IActionResult> ForgotPassword();
    }
}