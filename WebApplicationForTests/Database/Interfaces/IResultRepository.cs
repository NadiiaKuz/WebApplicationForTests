using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database.Interfaces
{
    public interface IResultRepository : IRepository<TestResult>
    {
        Task<IEnumerable<TestResult>> GetAllByUserLoginAsync(string login);
    }
}
