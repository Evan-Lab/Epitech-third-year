namespace AreaServer.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public UserAdminStatus Admin { get; set; } = UserAdminStatus.IsUser;

        public UserAccountStatus AccountStatus { get; set; }

        public DateTime DateCreation { get; set; }
        public UserGoogleStatus GoogleStatus { get; set; } = UserGoogleStatus.NotLinked;

    }
}
