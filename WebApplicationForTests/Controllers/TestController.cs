using Microsoft.AspNetCore.Mvc;
using WebApplicationForTests.Helper;
using WebApplicationForTests.Models;
using WebApplicationForTests.Models.ViewModels;
using WebApplicationForTests.Services;
using static System.Formats.Asn1.AsnWriter;

namespace WebApplicationForTests.Controllers
{
    public class TestController : Controller
    {
        private readonly TestService _testService;
        private readonly UserService _userService;

        public TestController(TestService testService, UserService userService)
        {
            _testService = testService;
            _userService = userService;

        }

        public async Task<IActionResult> Index()
        {
            var tests = await _testService.GetAllAsync();

            return View(tests);
        }

        public async Task<IActionResult> StartTest(int testId)
        {
            var test = await _testService.GetTestAsync(testId);

            return View(test);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTest(int testId, IFormCollection form)
        {
            var test = await _testService.GetTestAsync(testId);

            var userAnswers = new Dictionary<int, int>();

            foreach (var question in test.Questions)
            {
                if (form.TryGetValue($"question-{question.Id}", out var answerId))
                {
                    userAnswers.Add(question.Id, int.Parse(answerId));
                }
            }

            var score = _testService.CalculateScore(test, userAnswers);

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

        public IActionResult Results(string testTitle, decimal score)
        {
            var viewModel = new TestResultViewModel
            {
                TestTitle = testTitle,
                Score = score
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SaveLoginResults()
        {
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

        public async Task<IActionResult> MyResults()
        {
            var myResults = await _testService.GetResultsByLoginAsync(User.Identity.Name);

            return View(myResults);
        }
    }
}
