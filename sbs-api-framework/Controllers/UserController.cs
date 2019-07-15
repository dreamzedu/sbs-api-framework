using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using sbs_api.models;
using sbs_api.repository;

namespace sbs_api.Controllers
{
    [RoutePrefix("api/user")]
    
    public class UserController : ApiController
    {
        private IUserRepo repo;
        public UserController()
        {
            repo = new UserRepo();
        }

        [HttpGet()]
        [Route("{id}")]
        public User Get(int id)
        {
            return repo.GetUser(id);
        }

        [HttpGet()]
        public IEnumerable<User> GetAllUsers()
        {
            return repo.GetUsers();
        }

        // GET api/values
        [HttpGet()]
        [Route("roles")]
        public IEnumerable<Role> GetAllRoles()
        {
            return repo.GetAllRoles();
        }

        // GET api/values
        [HttpGet()]
        [Route("user-roles")]
        public IEnumerable<UserRole> GetUserRolesMapping()
        {
            return repo.GetUserRolesMapping();
        }

        // POST api/values
        [HttpPost()]
        public int Post([FromBody] User user)
        {
            return repo.InsertUser(user);
        }

        [HttpPost]
        [Route("update/{id}")]
        public void UpdateUser(int id, [FromBody] User user)
        {
            repo.UpdateUser(user);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public void DeleteUser(int id)
        {
            repo.DeleteUser(id);
        }
    }
}
