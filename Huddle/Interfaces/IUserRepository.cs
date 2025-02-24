namespace Huddle.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
        Task AddUser(User user);
    }
}
