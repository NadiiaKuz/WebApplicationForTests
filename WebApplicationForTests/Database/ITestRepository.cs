using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<IEnumerable<Test>> GetAllAsync();
        Task<Test> GetByIdAsync(int id);
    }
}
