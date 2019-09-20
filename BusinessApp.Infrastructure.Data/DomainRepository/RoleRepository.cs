using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entity.Users;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessApp.Infrastructure.Data.DomainRepository
{
    public class RoleRepository : IRoleRepository
    {
        private Role _role;
        private RoleManager<Role> _roleManager;
        //private IdentityResult<Role> _identityResult;
        private Task<IdentityResult> identityResult;

        public RoleRepository(Role role, RoleManager<Role> roleMAnager)
        {
            _role = role;
            _roleManager = roleMAnager;
        }
        public Role Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetByName(string name)
        {

            _role = await _roleManager.FindByNameAsync(name).ConfigureAwait(false);
            if (string.IsNullOrEmpty(_role.ConcurrencyStamp))
            {
                return _role.ConcurrencyStamp = "";
            }
            return _role.Name;
        }

        public Task<IdentityResult> Post(Role role)
        {
            //Task<IdentityResult> identityResult;
            if (string.IsNullOrEmpty(GetByName(role.Name).Result))
            {
                return this.identityResult = _roleManager.CreateAsync(role);
            }
            return this.identityResult;
        }

        public Role Put(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
