using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(string login)
        {
            return await _userRepository.GetByLoginAsync(login);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }
    }
}
