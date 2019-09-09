using BusinessApp.Core.Entitiy.Users;
using System.Collections.Generic;

namespace BusinessApp.Core.DomainService.AccountRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        Role GetById(int id);
        Role Post(Role role);
        Role Put(Role role);
        Role Delete(int id);
    }
}
