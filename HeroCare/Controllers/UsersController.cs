using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entity.Users;
using BusinessApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HeroCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserRepository Repo { get; }
        /*
            //TODO consider removing context 
            //context is decleared for UserExist() method
        */
        public HeroCareCoreContext Context { get; }

        public UsersController(HeroCareCoreContext context,
            IUserRepository repository)
        {
            Repo = repository;
            Context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await Repo.GetAll());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var _user = await Repo.GetById(id);
            if (_user == null)
            {
                return Ok("no users here");
            }
            return Ok(_user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            //TODO Refactor this method 
            if (UserExists(id))
            {
                try
                {
                    return Ok(await Repo.UpdateUser(id.ToString(), user));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return BadRequest();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            if (user != null)
            {
                var _user = await Repo.CreateUser(user);
                return CreatedAtAction("GetUser", new { id = user.Id }, _user);
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            return Ok(await Repo.DeleteUser(id.ToString()));

        }

        //TODO refactor this method
        private bool UserExists(int id)
        {
            return Context.User.Any(e => e.Id == id.ToString());
        }
    }
}
