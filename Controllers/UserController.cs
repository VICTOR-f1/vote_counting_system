using electronic_library_6.Domain.Entities;
using electronic_library_6.Domain.Services;
using electronic_library_6.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace electronic_library_6.Controllers
{
    public class UserController :Controller
    {
        private const int adminRoleId = 2;
        private const int clientRoleId = 1;
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Login ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            User? user = await userService.GetUserAsync(loginViewModel.Username, loginViewModel.Password);
            if (user is not null)
            {
                await SignIn(user);
                return RedirectToAction("Index", "Books");
            }
            try
            {
                ModelState.AddModelError("reg_error", $"Неверное имя пользователя или пароль");
                return View(loginViewModel);
            } 
            catch
            {
                ModelState.AddModelError("reg_error", $"Неверное имя пользователя или пароль");
                return View(loginViewModel);
            }
        }
        [HttpGet]
        public IActionResult Logout ()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public IActionResult Registration ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration (RegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }
            if (await userService.IsUserExistsAsync(registration.Username))
            {
                ModelState.AddModelError("user_exists", $"Имя пользователя {registration.Username} уже существует!");
                return View(registration);
            }
            try
            {
                await userService.RegistrationAsync(registration.Fullname, registration.Username, registration.Password);
                return RedirectToAction("RegistrationSuccess", "User");
            } 
            catch
            {
                ModelState.AddModelError("reg_error", $"Не удалось зарегистрироваться, попробуйте попытку регистрации позже");
                return View(registration);
            }
        }
        public IActionResult RegistrationSuccess ()
        {
            return View();
        }
        private async Task SignIn (User user)
        {
            string role = user.RoleId switch
            {
                adminRoleId => "OlderAdmin",
                clientRoleId => "JuniorAdmin",
                _ => throw new ApplicationException("invalid user role")
            };

            List<Claim> claims = new List<Claim>
            {
            new Claim("fullname", user.Fullname),
            new Claim("id", user.Id.ToString()),
            new Claim("role", role),
            new Claim("username", user.Login)
            };
            string authType = CookieAuthenticationDefaults.AuthenticationScheme;
            IIdentity identity = new ClaimsIdentity(claims, authType, "username", "role");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
        [HttpGet]
        public IActionResult AccessDenied ()
        {
            return View();
        }

    }
}
