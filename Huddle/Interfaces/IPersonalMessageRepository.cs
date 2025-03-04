using Huddle.Models;

namespace Huddle.Interfaces
{
    public interface IPersonalMessageRepository
    {
        // GET METHOD
        Task<List<PersonalMessages>> GetMessage(Guid userId, Guid friendId);
        Task<List<PersonalMessages>> GetFilteredMessage(Guid userId, Guid friendId, String filter);

        // POST METHOD
        Task PostMessage(PersonalMessages newMsg);

        // PUT METHOD
        Task UpdateMessage(Guid msgId, string newContent);
        
        // DELETE METHOD
        Task DeleteMessage(Guid msgId);

    }
}
