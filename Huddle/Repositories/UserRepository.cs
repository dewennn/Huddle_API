using Huddle.Context;
using Huddle.Interfaces;
using Huddle.Models;
using Microsoft.EntityFrameworkCore;

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

        // GET MULTIPLE USER WITH IDs
        public async Task<List<User>> GetUserByIds(List<Guid> ids)
        {
            return await _context.Users
                .Where(u => ids.Contains(u.Id))
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

        // CREATE NEW USER
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
