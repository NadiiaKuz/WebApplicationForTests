using WebApplicationForTests.Database.Interfaces;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Services
{
    public class TestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IResultRepository _resultRepository;
        private readonly ILogger _logger;

        public TestService(ITestRepository testRepository, IResultRepository resultRepository, ILogger<TestService> logger)
        {
            _testRepository = testRepository;
            _resultRepository = resultRepository;
            _logger = logger;
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            try
            {
                _logger.LogInformation("Fetching test with ID {TestId}", testId);
                return await _testRepository.GetByIdAsync(testId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching test with ID {TestId}", testId);
                throw;
            }
        }

        public decimal CalculateScore(Test test, Dictionary<int, int> userAnswers)
        {
            try
            {
                _logger.LogInformation("Calculating score for test ID {TestId}", test.Id);
                int score = 0;

                foreach (var question in test.Questions)
                {
                    if (userAnswers.TryGetValue(question.Id, out int selectedAnswerId) &&
                        question.Answers.Any(ac => ac.Id == selectedAnswerId && ac.IsCorrect))
                    {
                        score++;
                    }
                }

                decimal finalScore = (decimal)score / (decimal)test.Questions.Count;
                _logger.LogInformation("Score calculated for test ID {TestId} is {Score}", test.Id, finalScore);

                return finalScore;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating score for test ID {TestId}", test.Id);
                throw;
            }
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