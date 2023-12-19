using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationForTests.Models.ViewModels;

namespace WebApplicationForTests.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Navigating to the Test Index page.");
                return RedirectToAction("Index", "Test");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while navigating to the Test Index page.");
                return RedirectToAction("Error");
            }
        }

        public IActionResult Privacy()
        {
            try
            {
                _logger.LogInformation("Accessed Privacy page.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred on the Privacy page.");
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View(errorViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred on the Error page.");
                return View("CriticalError");
            }
        }
    }
}