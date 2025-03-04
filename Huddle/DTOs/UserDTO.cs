namespace Huddle.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string UserStatus { get; set; } = null!;
        public string OnlineStatus { get; set; } = null!;
        public string ProfilePictureUrl { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? AboutMe { get; set; }
    }
}
