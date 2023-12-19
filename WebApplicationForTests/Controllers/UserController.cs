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

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserAsync(model.Login);

                if (user is not null)
                {
                    ModelState.AddModelError(nameof(model.Login), "Login is already in use");
                    return View(model);
                }

                byte[] salt = PasswordHasher.GenerateSalt();
                string passwordHash = PasswordHasher.HashPasswordWithSalt(model.Password, salt);

                var newUser = new User()
                {
                    Login = model.Login,
                    PasswordHash = passwordHash,
                    Salt = Convert.ToBase64String(salt)
                };

                await _userService.AddUserAsync(newUser);

                return RedirectToAction("SignIn", "User", new LoginBindingModel()
                {
                    Login = model.Login,
                    Password = model.Password,
                });
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserAsync(model.Login);

                if (user is not null)
                {
                    var isCorrectPassword = PasswordHasher.IsCorrectPassword(user, model.Password);
                    
                    if (isCorrectPassword)
                    {
                        await SignInAsync(user);

                        if(CookieHelper.GetResults(HttpContext).Any())
                            return RedirectToAction("SaveLoginResults", "Test");

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Wrong login or password");
            }
            
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("CookieAuth");
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
