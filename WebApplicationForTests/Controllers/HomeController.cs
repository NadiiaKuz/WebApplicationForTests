using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationForTests.Models.ViewModels;
using WebApplicationForTests.Services;

namespace WebApplicationForTests.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TestService _testService;

        public HomeController(TestService testService, ILogger<HomeController> logger)
        {
            _testService = testService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var tests = await _testService.GetAllAsync();

            return View(tests);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}