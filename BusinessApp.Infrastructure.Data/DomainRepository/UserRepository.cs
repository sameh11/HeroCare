//using System.Threading.Tasks;
using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.Infrastructure.Data.DomainRepository
{
    public class UserRepository : IUserRepository
    {
        readonly HeroCareCoreContext _context;
        public UserRepository(HeroCareCoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync().ConfigureAwait(true);
        }

        public async Task<User> GetById(int id)
        {
            var _user = await _context.User.FindAsync(id).ConfigureAwait(true);

            if (_user == null)
            {
                return null;
            }
            return _user;
        }

        public async Task<User> CreateUser(User user)
        {
            if (user.Email != null)
            {
                //TODO Add validation 
                _ = await _context.User.AddAsync(user).ConfigureAwait(true);
                int _save = await _context.SaveChangesAsync().ConfigureAwait(true);
                if (_save != 0)
                {
                    return user;
                }
                else
                {
                    //Validation error
                    return null;
                }
            }
            else
            {
                //TODO
                return null;
            }
        }

        public async Task<User> UpdateUser(string id, User user)
        {
            if (id != user.Id)
            {
                return null;
            }
            var _changes = _context.Entry(user).State;
            _changes = EntityState.Modified;
            var _user = _context.User.Any(e => e.Id == id);
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_user)
                {
                    //TODO not found exception
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return user;
            //TODO Add validation 
            //int _id = id; 
            //if (_id != 0 && _id != null)
            //{               
            //    var _user = await _context.User.FindAsync(id);
            //    _user = user;
            //    int _save = await _context.SaveChangesAsync();
            //    if (_save != 0 )
            //    {
            //        return _user;
            //    }
            //    else
            //    {
            //        //TODO Validation erro
            //        return null; 
            //    }
            //}
            //else
            //{
            //    //TODO return error 
            //    return null;
            //}
            //throw new NotImplementedException();
        }

        public async Task<User> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(id).ConfigureAwait(true);
            if (user == null)
            {
                //TODO Add error exception 
                return null;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return user;
        }
    }
}
