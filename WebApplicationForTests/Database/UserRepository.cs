using Microsoft.EntityFrameworkCore;
using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {


            return await _context.Users
                //.Include(x => x.Questions)
                //.ThenInclude(x => x.Answers)
                //.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
