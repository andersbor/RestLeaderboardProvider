using System;
using System.Collections.Generic;
using System.Linq;
using leaderboardRestProvider.model;
using Microsoft.AspNetCore.Mvc;

namespace leaderboardRestProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private static int _nextId = 3;

        internal static readonly List<Score> SList = new List<Score>
        {
            new Score{ Id = 1, UserId = 1, Points = 100, Created = DateTime.Now},
            new Score{Id = 2, UserId = 1, Points = 132, Created = DateTime.Now}
        };

        // GET api/values
        [Route("{count:int?}")]
        public IEnumerable<Score> GetAll(int count = -1)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby
            IEnumerable<Score> ordered = SList.OrderBy(score => -score.Points);
            if (count < 0) return ordered;
            return ordered.Take(count);
        }

        // GET api/values/5
        /* Not necessary
        [Route("{id:int}")]
        public Score GetById(int id)
        {
            return SList.FirstOrDefault(score => score.Id == id);
        }
        */

        // POST api/values
        [HttpPost]
        public Score Post([FromBody] Score value)
        {
            if (!UsersController.UList.Exists(user => user.Id == value.UserId))
                return null;
            value.Id = _nextId++;
            value.Created = DateTime.Now;
            SList.Add(value);
            return value;
        }

        // PUT api/values/5
        /* Not necessary
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        */

        // DELETE api/values/5
        /* Not necessary
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */

        internal static int DeleteScoresByUserId(int userId)
        {
            return SList.RemoveAll(score => score.UserId == userId);
        }
    }
}
