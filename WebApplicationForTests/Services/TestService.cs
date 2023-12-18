using WebApplicationForTests.Database;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Services
{
    public class TestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            return await _testRepository.GetByIdAsync(testId);
        }

        public int CalculateScore(Test test, Dictionary<int, int> userAnswers)
        {
            int score = 0;
            foreach (var question in test.Questions)
            {
                if (userAnswers.TryGetValue(question.Id, out int selectedAnswerId) &&
                    question.Answers.Any(ac => ac.Id == selectedAnswerId && ac.IsCorrect))
                {
                    score++;
                }
            }
            return score;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _testRepository.GetAllAsync();
        }
    }
}
