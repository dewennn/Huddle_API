using Huddle.DTOs;
using Huddle.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huddle.Interfaces
{
    public interface IUserService
    {
        // VALIDATION SERVICE
        Task<string?> ValidateUserByEmailPass(string email, string password);
        Guid? ValidateToken(string? token);

        // GET SERVICE
        Task<User?> GetUserByID(Guid? id);
        Task<UserDTO?> GetUserDTO(Guid? id);
        Task<List<UserDTO>?> GetUserSentFriendRequest(Guid? id);
        Task<List<UserDTO>?> GetUserReceivedFriendRequest(Guid? id);
        Task<List<UserDTO>?> GetUserFriendList(Guid? id);

        // POST SERVICE
        Task<string?> AddUser(User user);
        Task AddFriendRequestWithUsername(Guid userId, string targetUsername);
        Task AddFriendship(Guid userId, Guid friendId);

        // DELETE SERVICE
        Task RemoveFriendship(Guid userId, Guid friendId);
        Task RemoveFriendshipRequest(Guid senderId, Guid receiverId);
    }
}
