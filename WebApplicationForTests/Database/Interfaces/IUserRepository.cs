using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByLoginAsync(string login);
    }
}
