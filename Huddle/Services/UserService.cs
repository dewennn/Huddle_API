using Huddle.Interfaces;
using Huddle.Models;
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
        public async Task<string?> AddUser(User user)
        {
            user.PasswordHashed = _passwordService.HashPassworad(user.PasswordHashed);
            await _userRepository.AddUser(user);

            return _jwtService.GenerateToken(user.Id, user.Email);
        }

        // VALIDATE USER
        public async Task<string?> ValidateUserByEmailPass(string email, string inputPassword)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (
                user != null &&
                _passwordService.VerifyPassword(user.PasswordHashed, inputPassword)
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

        // GET FRIEND BY ID
        public async Task<List<Friendship>?> GetUserFriendList(Guid id)
        {
            var friendships = await _userRepository.GetUserFriendList(id);
            if (friendships != null)
            {
                return friendships;
            }

            return null;
        }
    }
}
