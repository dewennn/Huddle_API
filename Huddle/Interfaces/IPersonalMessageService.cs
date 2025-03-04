using Huddle.Models;

namespace Huddle.Interfaces
{
    public interface IPersonalMessageService
    {
        // GET METHOD
        Task<List<PersonalMessages>> GetMessage(Guid userId, Guid friendId);
        Task<List<PersonalMessages>> GetFilteredMessage(Guid userId, Guid friendId, String filter);

        // POST METHOD
        Task PostMessage(Guid senderId, Guid receiverId, string content);
    }
}
