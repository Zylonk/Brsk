namespace ClientWeb.Models
{
    public class User
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
    }
}
