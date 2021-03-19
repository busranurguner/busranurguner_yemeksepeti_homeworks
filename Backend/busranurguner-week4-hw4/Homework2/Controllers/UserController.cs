using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Homework2.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                UserId = 1,
                FirstName = "Adam",
                LastName = "Smith"
            },
            new User
            {
                UserId = 2,
                FirstName = "John",
                LastName = "Michael"
            },
            new User
            {
                UserId = 3,
                FirstName = "Robert",
                LastName = "Draco"
            }
        };

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        [HttpGet("{userId}")]
        public User Get(int userId)
        {
            return _users.Where(e => e.UserId == userId).FirstOrDefault();
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            var userId = _users.Select(e => e.UserId).Max() + 1;
            _users.Add(new Homework2.User
            {
                UserId = userId,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [HttpPut("{userId}")]
        public void Put(int userId, [FromBody] User user)
        {
            var existingUser = _users.Where(e => e.UserId == userId).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
            }
        }

        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
            var existingUser = _users.Where(e => e.UserId == userId).FirstOrDefault();
            if (existingUser != null)
            {
                _users.Remove(existingUser);
            }
        }
    }
}
