using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Models.Auth;
using NoteApp.Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly INoteService _noteService;

        public HomeController(IUserService userService, INoteService noteService)
        {
            _userService = userService;
            _noteService = noteService;
        }
        [Authorize]
        public IActionResult Index()
        {
            var notes = _noteService.GetAllNotes();
            ViewData["Message"] = notes.Message;
            ViewData["Status"] = notes.Status;

            return View(notes.Data);
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
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model);
            var user = response.Data;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            if (user.RoleName == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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