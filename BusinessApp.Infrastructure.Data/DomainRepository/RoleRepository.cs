using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entity.Users;
using System;
using System.Collections.Generic;

namespace BusinessApp.Infrastructure.Data.DomainRepository
{
    public class RoleRepository : IRoleRepository
    {
        //private readonly IRoleRepository _repo;
        public RoleRepository()
        {
            //_repo = roleRepository;
        }
        public Role Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Role Post(Role role)
        {
            throw new NotImplementedException();
        }

        public Role Put(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
