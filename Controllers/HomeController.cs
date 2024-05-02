using NoteApp.Models;
using System.Diagnostics;
using NoteApp.Models.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly INoteService _noteService;
        private readonly INotyfService _notyf;
        public HomeController(IUserService userService, INoteService noteService, INotyfService notyf)
        {
            _userService = userService;
            _noteService = noteService;
            _notyf = notyf;
        }
       
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            var response = _userService.Register(model);

            if (response.Status == false)
            {
                _notyf.Error(response.Message);

                return View();
            }

            _notyf.Success(response.Message);
            return RedirectToAction("Index", "Note");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model);
            if(response.Status == false)
            {
                _notyf.Error(response.Message);
                return View();
            }
            var user = response.Data;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            _notyf.Success(response.Message);

            if (user.RoleName == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }
            return RedirectToAction("Create", "Note");
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _notyf.Success("You Have succesfully log out!");
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult AdminDashBoard()
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