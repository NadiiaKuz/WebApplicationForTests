using Microsoft.EntityFrameworkCore;
using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database
{
    public class ResultRepository : Repository<TestResult>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TestResult>> GetAllByUserLoginAsync(string login)
        {
            return await _dbSet
                .Include(x => x.Test)
                .Include(x => x.User)
                .Where(x => x.User.Login == login)
                .ToListAsync();
        }
    }
}