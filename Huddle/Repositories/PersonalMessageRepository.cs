using Huddle.Context;
using Huddle.Interfaces;
using Huddle.Models;
using Microsoft.EntityFrameworkCore;

namespace Huddle.Repositories
{
    public class PersonalMessageRepository : IPersonalMessageRepository
    {
        private readonly HuddleDBContext _context;
        public PersonalMessageRepository(HuddleDBContext context)
        {
            _context = context;
        }

        // GET ALL MESSAGE
        public async Task<List<PersonalMessages>> GetMessage(Guid userId, Guid friendId)
        {
            return await _context.PersonalMessages.
                Where(m=>
                    (m.SenderId == userId && m.ReceiverId == friendId) ||
                    (m.SenderId == friendId && m.ReceiverId == userId))
                .OrderBy(m => m.DateCreated)
                .ToListAsync();
        }
        // GET FILTERED MESSAGE
        public async Task<List<PersonalMessages>> GetFilteredMessage(Guid userId, Guid friendId, string filter)
        {
            return await _context.PersonalMessages
                .Where(m =>
                    ((m.SenderId == userId && m.ReceiverId == friendId) ||
                    (m.SenderId == friendId && m.ReceiverId == userId))
                    && m.Content.Contains(filter)
                )
                .OrderBy(m => m.DateCreated)
                .ToListAsync();
        }

        // POST NEW MESSAGE
        public async Task PostMessage(PersonalMessages newMsg)
        {
            await _context.PersonalMessages.AddAsync(newMsg);
            await _context.SaveChangesAsync();
        }

        // UPDATE A MESSAGE
        public async Task UpdateMessage(Guid msgId, string newContent)
        {
            PersonalMessages? target = await _context.PersonalMessages.FindAsync(msgId);

            if (target == null) return;
            target.Content = newContent;

            await _context.SaveChangesAsync();
        }

        // DELETE MESSAGE
        public Task DeleteMessage(Guid msgId)
        {
            throw new NotImplementedException();
        }
    }
}
