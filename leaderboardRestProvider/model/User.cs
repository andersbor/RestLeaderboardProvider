using System;

namespace leaderboardRestProvider.model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public DateTime Enrolled { get; set; }
    }
}
