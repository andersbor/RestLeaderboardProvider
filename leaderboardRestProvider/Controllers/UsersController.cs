using System;
using System.Collections.Generic;
using System.Linq;
using leaderboardRestProvider.model;
using Microsoft.AspNetCore.Mvc;

namespace leaderboardRestProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static int _nextId = 3;
        internal static readonly List<User> UList = new List<User>
        {
            new User {Id = 1, Username = "anders", Enrolled = DateTime.Now},
            new User {Id = 2, Username = "peter", Enrolled = DateTime.Now}
        };

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UList;
        }

        // GET: api/User/5
        [HttpGet("{id:int}", Name = "Get")]
        public User Get(int id)
        {
            return UList.FirstOrDefault(user => user.Id == id);
        }

        [Route("{userId}/scores")]
        public IEnumerable<Score> GetScoresByUserId(int userId)
        {
            return ScoresController.SList.FindAll(score => score.UserId == userId);
        }

        // POST: api/User
        [HttpPost]
        public User Post([FromBody] User value)
        {
            value.Id = _nextId++;
            value.Enrolled = DateTime.Now;
            UList.Add(value);
            return value;
        }

        // PUT: api/User/5
        [HttpPut("{id:int}")]
        public User Put(int id, [FromBody] User value)
        {
            User u = UList.FirstOrDefault(user => user.Id == id);
            if (u == null) return null;
            u.PasswordHashed = value.PasswordHashed;
            u.Username = value.Username;
            return u;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}")]
        public User Delete(int id)
        {
            User u = UList.FirstOrDefault(user => user.Id == id);
            if (u == null) return null;
            ScoresController.DeleteScoresByUserId(id);
            UList.Remove(u);
            return u;
        }
    }
}
