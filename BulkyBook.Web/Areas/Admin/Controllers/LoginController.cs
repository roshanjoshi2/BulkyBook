using BulkyBook.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Operations;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();  
        }
        [HttpGet]
        public IActionResult PerformLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PerformLogin( Login logdetails)
        {
            if ((!string.IsNullOrEmpty(logdetails.Username)) && (!string.IsNullOrEmpty(logdetails.Password)))
            {
                if ((logdetails.Username.Equals("admin") && logdetails.Password.Equals("admin")))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, logdetails.Username),
                    new Claim(ClaimTypes.Role, "User"),
                };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(10),
                    };
                     HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(logdetails);
        }

        public IActionResult Logout()
        {
             HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
    }
}
