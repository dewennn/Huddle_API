using Huddle.DTOs;
using Huddle.Models;

namespace Huddle.Interfaces
{
    public interface IUserRepository
    {
        // GET REQUESTS
        Task<User?> GetUserById(Guid? id);
        Task<UserDTO?> GetUserDTO(Guid? id);
        Task<Guid?> GetUserIdByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<List<Guid>?> GetUserSentFriendRequests(Guid? id);
        Task<List<Guid>?> GetUserReceivedFriendRequests(Guid? id);
        Task<List<Guid>?> GetUserFriendIdList(Guid? id);
        Task<List<UserDTO>> GetUserListDTOByIds(List<Guid> ids);
        Task<Friendship?> GetFriendship(Guid userOne, Guid userTwo);


        // POST REQUESTS
        Task AddUser(User user);
        Task AddFriendshipRequest(FriendshipRequest newRequest);
        Task AddFriendship(Friendship friendship);


        // DELETE REQUESTS
        Task DeleteFriendship(Friendship friendship);
        Task DeleteFriendshipRequest(FriendshipRequest friendshipRequest);
    }
}
