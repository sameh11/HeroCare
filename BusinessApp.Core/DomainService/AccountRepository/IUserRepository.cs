//using BusinessApp.Core.DomainService.UserRepository;
using BusinessApp.Core.Entitiy.Users;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;


namespace BusinessApp.Core.DomainService.AccountRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(string id, User user);
        Task<User> DeleteUser(string id);
    }
}