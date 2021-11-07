using LibraryUsageImplementation.Handlers;
using LibraryUsageImplementation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryUsageImplementation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersHandler _handler;

        public UsersController(IUsersHandler usersHandler)
        {
            _handler = usersHandler;
        }

        [HttpPost]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            try
            {
                return _handler.CreateUser(user);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<User> GetUser([FromRoute] int id)
        {
            var result = _handler.GetUser(id);
            if (result is null)
            {
                return BadRequest("User not found");
            }

            return result;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _handler.GetAllUsers();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<User> DeleteUser([FromRoute] int id)
        {
            var result = _handler.DeleteUser(id);
            if (result is null)
            {
                return BadRequest("User not found");
            }

            return result;
        }

        [HttpPut]
        public ActionResult<User> ModifyUser([FromBody] User user)
        {
            try
            {
                var result = _handler.ModifyUser(user);
                if(result is null)
                {
                    return BadRequest("User not found");
                }

                return result;
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
