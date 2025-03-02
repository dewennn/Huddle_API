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
        Task<List<FriendListDTO>?> GetUserSentFriendRequest(Guid? id);
        Task<List<FriendListDTO>?> GetUserReceivedFriendRequest(Guid? id);
        Task<List<FriendListDTO>?> GetUserFriendList(Guid? id);

        // POST SERVICE
        Task<string?> AddUser(User user);
        Task AddFriendRequestWithUsername(Guid userId, string targetUsername);
    }
}
