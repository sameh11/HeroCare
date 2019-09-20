using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusinessApp.Core.ApplicationService.IService
{
    public interface IRegisterService
    {
        Task<object> Register(User user);
        Task<object> ConfirmEmail(string userId, string code);
        public Task<string> GenerateCode(User user);
        Task<IdentityResult> AddRoleToUser(User user, Role role);
    }
}