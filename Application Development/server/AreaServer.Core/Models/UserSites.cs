namespace AreaServer.Core.Models
{
    public class UserSites
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserKey { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; }

        public User User { get; set; }
        public Services Services { get; set; }

    }
}
