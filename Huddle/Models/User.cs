using System;
using System.Collections.Generic;

namespace Huddle.Models
{
    public partial class User
    {
        public User()
        {}

        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHashed { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string? DateOfBirth { get; set; }
        public string? DisplayName { get; set; }
        public string? AboutMe { get; set; }
        public string? UserStatus { get; set; }
        public string? OnlineStatus { get; set; }
    }
}
