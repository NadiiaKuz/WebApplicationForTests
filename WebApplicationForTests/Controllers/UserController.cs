using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationForTests.Models;
using WebApplicationForTests.Models.BindingModels;
using WebApplicationForTests.Services;
using WebApplicationForTests.Helper;

namespace WebApplicationForTests.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _logger.LogInformation("Attempting to register new user {Login}", model.Login);
                var user = await _userService.GetUserAsync(model.Login);

                if (user != null)
                {
                    ModelState.AddModelError(nameof(model.Login), "Login is already in use");
                    return View(model);
                }

                byte[] salt = PasswordHasher.GenerateSalt();
                string passwordHash = PasswordHasher.HashPasswordWithSalt(model.Password, salt);

                var newUser = new User
                {
                    Login = model.Login,
                    PasswordHash = passwordHash,
                    Salt = Convert.ToBase64String(salt)
                };

                await _userService.AddUserAsync(newUser);
                _logger.LogInformation("User {Login} successfully registered", model.Login);

                return RedirectToAction("SignIn", "User", new LoginBindingModel { Login = model.Login, Password = model.Password });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user {Login}", model.Login);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginBindingModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _logger.LogInformation("User {Login} attempting to sign in.", model.Login);
                var user = await _userService.GetUserAsync(model.Login);

                if (user != null)
                {
                    var isCorrectPassword = PasswordHasher.IsCorrectPassword(user, model.Password);

                    if (isCorrectPassword)
                    {
                        await SignInAsync(user);
                        _logger.LogInformation("User {Login} successfully signed in.", model.Login);

                        if (CookieHelper.GetResults(HttpContext).Any())
                            return RedirectToAction("SaveLoginResults", "Test");

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Wrong login or password");
                _logger.LogWarning("Failed sign in attempt for user {Login}.", model.Login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sign in for user {Login}.", model.Login);
                ModelState.AddModelError("", "An error occurred while processing your request.");
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            try
            {
                await HttpContext.SignOutAsync("CookieAuth");
                _logger.LogInformation("User signed out successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sign out.");
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task SignInAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("UserId", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}