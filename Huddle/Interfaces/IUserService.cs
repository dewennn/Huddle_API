using Microsoft.AspNetCore.Mvc;

namespace Huddle.Interfaces
{
    public interface IUserService
    {
        Task<string?> ValidateUserByEmailPass(string email, string password);
        Task AddUser(User user);
        Task<User?> GetUserByID(Guid id);
    }
}
