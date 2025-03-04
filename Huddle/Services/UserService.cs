using Huddle.DTOs;
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
        // VALIDATE USER
        public async Task<string?> ValidateUserByEmailPass(string email, string inputPassword)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (
                user != null &&
                _passwordService.VerifyPassword(user.PasswordHashed, inputPassword)
            )
            {
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
        // GET USER DTO
        public async Task<UserDTO?> GetUserDTO(Guid? id)
        {
            var user = await _userRepository.GetUserDTO(id);

            if (user == null) return null;

            return user;

        }
        // GET USER SENT FRIEND REQUEST
        public async Task<List<UserDTO>?> GetUserSentFriendRequest(Guid? id)
        {
            var friendIds = await _userRepository.GetUserSentFriendRequests(id);

            if (friendIds == null)
            {
                return null;
            }

            List<UserDTO> friends = await _userRepository.GetUserListDTOByIds(friendIds);

            return friends;
        }
        // GET USER RECEIVED FRIEND REQUEST
        public async Task<List<UserDTO>?> GetUserReceivedFriendRequest(Guid? id)
        {
            var friendIds = await _userRepository.GetUserReceivedFriendRequests(id);

            if (friendIds == null)
            {
                return null;
            }

            List<UserDTO> friends = await _userRepository.GetUserListDTOByIds(friendIds);

            return friends;
        }
        // GET USER FRIEND LIST
        public async Task<List<UserDTO>?> GetUserFriendList(Guid? id)
        {
            var friendIds = await _userRepository.GetUserFriendIdList(id);

            if (friendIds == null)
            {

                return null;
            }

            List<UserDTO> friends = await _userRepository.GetUserListDTOByIds(friendIds);

            return friends;
        }


        // ADD NEW USER
        public async Task<string?> AddUser(User user)
        {
            user.PasswordHashed = _passwordService.HashPassword(user.PasswordHashed);
            await _userRepository.AddUser(user);

            return _jwtService.GenerateToken(user.Id, user.Email);
        }
        // ADD NEW FRIENDSHIP REQUESTS
        public async Task AddFriendRequestWithUsername(Guid userId, string targetUsername)
        {
            Guid? friendId = await _userRepository.GetUserIdByUsername(targetUsername);

            if (friendId == null) throw new ArgumentException("User not found");

            await _userRepository.AddFriendshipRequest(new FriendshipRequest(userId, friendId.Value));
        }
        // ADD NEW FRIENDSHIP
        public async Task AddFriendship(Guid userId, Guid friendId)
        {
            await _userRepository.AddFriendship(new Friendship(userId, friendId));
        }


        // DELETE FRIENDSHIP
        public async Task RemoveFriendship(Guid userId, Guid friendId)
        {
            Friendship target = await _userRepository.GetFriendship(userId, friendId);
            await _userRepository.DeleteFriendship(target);
        }
        // DELETE FRIENDSHIP REQUEST
        public async Task RemoveFriendshipRequest(Guid senderId, Guid receiverId)
        {
            await _userRepository.DeleteFriendshipRequest(new FriendshipRequest(senderId, receiverId));
        }

    }
}
