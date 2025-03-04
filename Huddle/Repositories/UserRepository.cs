    using Huddle.Context;
using Huddle.DTOs;
using Huddle.Interfaces;
using Huddle.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Versioning;

namespace Huddle.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HuddleDBContext _context;

        public UserRepository(HuddleDBContext context)
        {
            _context = context;
        }

        // GET USER BY EMAIL
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        // GET USER BY ID
        public async Task<User?> GetUserById(Guid? id)
        {
            if (id == null) return null;

            return await _context.Users.FindAsync(id);
        }
        // GET USER DTO
        public async Task<UserDTO?> GetUserDTO(Guid? id)
        {
            if (id == null) return null;
            return await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    DisplayName = u.DisplayName,
                    UserStatus = u.UserStatus,
                    OnlineStatus = u.OnlineStatus,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    DateCreated = u.DateCreated,
                    AboutMe = u.AboutMe
                })
                .FirstOrDefaultAsync();
        }
        // GET USER SENT FRIEND REQUEST - ID LIST
        public async Task<List<Guid>?> GetUserSentFriendRequests(Guid? id)
        {
            if (!id.HasValue) return null;

            return await _context.FriendshipRequests
                    .Where(f => f.SenderId == id)
                    .Select(f => f.ReceiverId)
                    .ToListAsync();
        }
        // GET USER RECEIVED FRIEND REQUEST - ID LIST
        public async Task<List<Guid>?> GetUserReceivedFriendRequests(Guid? id)
        {
            if (!id.HasValue) return null;

            return await _context.FriendshipRequests
                    .Where(f => f.ReceiverId == id)
                    .Select(f => f.SenderId)
                    .ToListAsync();
        }
        // GET USER FRIEND ID LIST
        public async Task<List<Guid>?> GetUserFriendIdList(Guid? id)
        {
            if (!id.HasValue) return null;

            return await _context.Friendships
                    .Where(f => f.UserOneId == id || f.UserTwoId == id)
                    .Select(f => f.UserOneId == id? f.UserTwoId : f.UserOneId)
                    .ToListAsync();
        }
        // GET USER FRIENDs DTO WITH IDs
        public async Task<List<UserDTO>> GetUserListDTOByIds(List<Guid> ids)
        {
            return await _context.Users
                .Where(u => ids.Contains(u.Id))
                .Select(u => new UserDTO { 
                    Id = u.Id,
                    Username = u.Username,
                    DisplayName = u.DisplayName,
                    UserStatus = u.UserStatus,
                    OnlineStatus = u.OnlineStatus,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    DateCreated = u.DateCreated,
                    AboutMe = u.AboutMe
                })
                .ToListAsync();
        }
        // GET USER ID w USERNAME
        public async Task<Guid?> GetUserIdByUsername(string username)
        {
            return await _context.Users
                .Where (u => u.Username == username)
                .Select(u => (Guid?)u.Id)
                .FirstOrDefaultAsync();
        }
        // GET FRIENDSHIP
        public async Task<Friendship?> GetFriendship(Guid userOne, Guid userTwo)
        {
            return await _context.Friendships
                .Where(f =>
                    (f.UserOneId == userOne && f.UserTwoId == userTwo) ||
                    (f.UserOneId == userTwo && f.UserTwoId == userOne))
                .FirstOrDefaultAsync();
        }


        // POST NEW USER
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        // POST NEW FRIENDSHIP REQUEST
        public async Task AddFriendshipRequest(FriendshipRequest newRequest)
        {
            _context.FriendshipRequests.Add(newRequest);
            await _context.SaveChangesAsync();
        }
        // POST NEW FRIENDSHIP
        public async Task AddFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }


        // DELETE FRIENDSHIP
        public async Task DeleteFriendship(Friendship friendship)
        {
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }
        // DELETE FRIENDSHIP REQUEST
        public async Task DeleteFriendshipRequest(FriendshipRequest friendshipRequest)
        {
            _context.FriendshipRequests.Remove(friendshipRequest);
            await _context.SaveChangesAsync();
        }
    }
}
