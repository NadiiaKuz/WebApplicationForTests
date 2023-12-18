using Microsoft.AspNetCore.Mvc;
using WebApplicationForTests.Models.ViewModels;
using WebApplicationForTests.Services;

namespace WebApplicationForTests.Controllers
{
    public class TestController : Controller
    {
        private readonly TestService _testService;

        public TestController(TestService testService)
        {
            _testService = testService;
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

            int score = _testService.CalculateScore(test, userAnswers);

            return RedirectToAction("Results", new { testTitle = test.Title, testId = testId, score = score });
        }

        public IActionResult Results(string testTitle, int testId, int score)
        {
            var viewModel = new TestResultViewModel
            {
                TestId = testId,
                TestTitle = testTitle,
                Score = score
            };

            return View(viewModel);
        }
    }
}
