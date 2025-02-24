using Huddle.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Huddle.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly PasswordService _passwordService;

        public UserService(
            IUserRepository userRepository,
            JwtService jwtService,
            PasswordService passwordService
        ){   
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        // ADD NEW USER
        public async Task AddUser(User user)
        {
            user.Password = _passwordService.HashPassworad(user.Password);
            await _userRepository.AddUser(user);
        }

        // VALIDATE USER
        public async Task<string?> ValidateUserByEmailPass(string email, string inputPassword)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (
                user != null &&
                _passwordService.VerifyPassword(user.Password, inputPassword)
            ){
                return _jwtService.GenerateToken(user.Id, user.Email);
            }

            return null;
        }

        // GET USER BY ID
        public async Task<User?> GetUserByID(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
