using Huddle.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huddle.Interfaces
{
    public interface IUserService
    {
        Task<string?> ValidateUserByEmailPass(string email, string password);
        Task<string?> AddUser(User user);
        Task<User?> GetUserByID(Guid? id);
        Task<List<User>?> GetUserFriendList(Guid? id);
        Guid? ValidateToken(string? token);
    }
}
