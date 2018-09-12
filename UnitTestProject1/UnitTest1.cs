using System.Collections.Generic;
using System.Linq;
using leaderboardRestProvider.Controllers;
using leaderboardRestProvider.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UsersController usersController = new UsersController();
            IEnumerable<User> users = usersController.Get();
            Assert.AreEqual(2, users.Count());

            User user = usersController.Get(1);
            Assert.AreEqual("anders", user.Username);
            User noUser = usersController.Get(100);
            Assert.IsNull(noUser);

            IEnumerable<Score> scores = usersController.GetScoresByUserId(1);
            Assert.AreEqual(2, scores.Count());

            user = new User {Username = "martin"};
            User newUser = usersController.Post(user);
            Assert.AreEqual(3, newUser.Id);
            Assert.AreEqual("martin", newUser.Username);

            users = usersController.Get();
            Assert.AreEqual(3, users.Count());

            User alteredUser = new User {Username = "andersb"};
            user = usersController.Put(1, alteredUser);
            Assert.AreEqual("andersb", user.Username);

            noUser = usersController.Delete(100);
            Assert.IsNull(noUser);

            user = usersController.Delete(1);
            Assert.AreEqual("andersb", user.Username);

        }
    }
}
