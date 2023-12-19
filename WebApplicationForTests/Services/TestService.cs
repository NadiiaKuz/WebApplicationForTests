using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Services
{
    public class TestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IResultRepository _resultRepository;

        public TestService(ITestRepository testRepository, IResultRepository resultRepository)
        {
            _testRepository = testRepository;
            _resultRepository = resultRepository;
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            return await _testRepository.GetByIdAsync(testId);
        }

        public decimal CalculateScore(Test test, Dictionary<int, int> userAnswers)
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
            return (decimal)score/(decimal)test.Questions.Count;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _testRepository.GetAllAsync();
        }

        public async Task AddUserTestResultAsync(TestResult testResult)
        {
            await _resultRepository.AddAsync(testResult);
        }

        public async Task<IEnumerable<TestResult>> GetResultsByLoginAsync(string login)
        {
            return await _resultRepository.GetAllByUserLoginAsync(login);
        }
    }
}
