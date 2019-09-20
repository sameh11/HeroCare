using BusinessApp.Core.Entity.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessApp.Core.DomainService.AccountRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        //Role GetById(int id); 
        Task<string> GetByName(string name);
        Task<IdentityResult> Post(Role role);
        //Role Post(Role role);
        Role Put(Role role);
        Role Delete(int id);
    }
}
