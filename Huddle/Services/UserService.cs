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

        // VALIDATE TOKEN
        public Guid? ValidateToken(string? token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var claims = _jwtService.ValidateToken(token); // Validate and extract claims

            if (claims == null) return null;

            return claims.Value.UserId;
        }

        // ADD NEW USER
        public async Task<string?> AddUser(User user)
        {
            user.PasswordHashed = _passwordService.HashPassword(user.PasswordHashed);
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
        public async Task<User?> GetUserByID(Guid? id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        // GET USER FRIEND LIST
        public async Task<List<User>?> GetUserFriendList(Guid? id)
        {
            var friendIds = await _userRepository.GetUserFriendIdList(id);

            if (friendIds == null)
            {

                return null;
            }

            List<User> friends = await _userRepository.GetUserByIds(friendIds);

            return friends;
        }
    }
}
