using Microsoft.AspNetCore.Mvc;
using WebApplicationForTests.Helper;
using WebApplicationForTests.Models;
using WebApplicationForTests.Models.ViewModels;
using WebApplicationForTests.Services;

namespace WebApplicationForTests.Controllers
{
    public class TestController : Controller
    {
        private readonly TestService _testService;
        private readonly UserService _userService;
        private readonly ILogger _logger;

        public TestController(TestService testService, UserService userService, ILogger<TestController> logger)
        {
            _testService = testService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all tests for the Index view.");
                var tests = await _testService.GetAllAsync();

                return View(tests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Index action of TestController.");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> StartTest(int testId)
        {
            try
            {
                _logger.LogInformation("Starting test with ID {TestId}", testId);
                var test = await _testService.GetTestAsync(testId);

                return View(test);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while starting test with ID {TestId}", testId);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTest(int testId, IFormCollection form)
        {
            try
            {
                _logger.LogInformation("Submitting test with ID {TestId}", testId);
                var test = await _testService.GetTestAsync(testId);

                var userAnswers = new Dictionary<int, int>();

                foreach (var question in test.Questions)
                {
                    if (form.TryGetValue($"question-{question.Id}", out var answerId))
                        userAnswers.Add(question.Id, int.Parse(answerId));
                }

                var score = _testService.CalculateScore(test, userAnswers);
                _logger.LogInformation("Test ID {TestId} scored {Score}", testId, score);

                if (User.Identity.IsAuthenticated)
                {
                    var result = new TestResult()
                    {
                        User = await _userService.GetUserAsync(User.Identity.Name),
                        Test = test,
                        Score = score,
                        Date = DateTime.UtcNow
                    };
                    await _testService.AddUserTestResultAsync(result);
                }
                else
                {
                    CookieHelper.AddResult(HttpContext, test.Id, score);
                }

                return RedirectToAction("Results", new { testTitle = test.Title, score = score });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while submitting test with ID {TestId}", testId);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Results(string testTitle, decimal score)
        {
            try
            {
                _logger.LogInformation("Displaying results for test {TestTitle}", testTitle);

                var viewModel = new TestResultViewModel
                {
                    TestTitle = testTitle,
                    Score = score
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while displaying results for test {TestTitle}", testTitle);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> SaveLoginResults()
        {
            try
            {
                _logger.LogInformation("Saving login results for user {UserName}", User.Identity.Name);
                var results = CookieHelper.GetResults(HttpContext);

                if (results.Any())
                {
                    var user = await _userService.GetUserAsync(User.Identity.Name);

                    foreach (var result in results)
                    {
                        var test = await _testService.GetTestAsync(result.TestId);
                        var userResult = new TestResult()
                        {
                            User = user,
                            Test = test,
                            Score = result.Score,
                            Date = DateTime.UtcNow
                        };
                        await _testService.AddUserTestResultAsync(userResult);
                    }

                    CookieHelper.ClearResults(HttpContext);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving login results for user {UserName}", User.Identity.Name);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> MyResults()
        {
            try
            {
                _logger.LogInformation("Fetching results for user {UserName}", User.Identity.Name);
                var myResults = await _testService.GetResultsByLoginAsync(User.Identity.Name);

                return View(myResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching results for user {UserName}", User.Identity.Name);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
