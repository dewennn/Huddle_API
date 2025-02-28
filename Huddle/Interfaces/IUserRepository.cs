using Huddle.DTOs;
using Huddle.Models;

namespace Huddle.Interfaces
{
    public interface IUserRepository
    {
        // GET REQUESTS
        Task<User?> GetUserById(Guid? id);
        Task<Guid?> GetUserIdByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<List<Guid>?> GetUserSentFriendRequests(Guid? id);
        Task<List<Guid>?> GetUserReceivedFriendRequests(Guid? id);
        Task<List<Guid>?> GetUserFriendIdList(Guid? id);
        Task<List<FriendListDTO>> GetFriendListDTOByIds(List<Guid> ids);


        // POST REQUESTS
        Task AddUser(User user);
        Task AddFriendshipRequest(FriendshipRequest newRequest);
        Task AddFriendship(Friendship friendship);
    }
}
