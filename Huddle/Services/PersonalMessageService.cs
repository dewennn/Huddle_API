using Huddle.Interfaces;
using Huddle.Models;

namespace Huddle.Services
{
    public class PersonalMessageService : IPersonalMessageService
    {
        private readonly IPersonalMessageRepository _repository;
        public PersonalMessageService(IPersonalMessageRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<PersonalMessages>> GetMessage(Guid userId, Guid friendId)
        {
            return await _repository.GetMessage(userId, friendId);
        }
        public Task<List<PersonalMessages>> GetFilteredMessage(Guid userId, Guid friendId, string filter)
        {
            throw new NotImplementedException();
        }

        public async Task PostMessage(Guid senderId, Guid receiverId, string content)
        {
            await _repository.PostMessage(new PersonalMessages(senderId, receiverId, content));
        }
    }
}
