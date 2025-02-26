using Microsoft.AspNetCore.Identity;

namespace Huddle.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
