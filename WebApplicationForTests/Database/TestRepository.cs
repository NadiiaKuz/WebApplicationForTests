using Microsoft.EntityFrameworkCore;
using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}