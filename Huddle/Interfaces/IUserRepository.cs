using Huddle.Models;

namespace Huddle.Interfaces
{
    public interface IUserRepository
    {   
        Task<User?> GetUserByEmail(string email);
        Task<List<User>> GetUserByIds(List<Guid> ids);
        Task<User?> GetUserById(Guid? id);
        Task<List<Guid>?> GetUserFriendIdList(Guid? id);
        Task AddUser(User user);
    }
}
