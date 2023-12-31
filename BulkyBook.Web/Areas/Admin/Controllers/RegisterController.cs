﻿using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using BulkyBook.DataAcess;
using Bulkybook.DataAcess.Repository;
using NuGet.Versioning;
using BulkyBook.Models;
using Bulkybook.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using System.Security.Cryptography;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterController(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult PerformLogin()
        {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> PerformLogin(RegisterVM userdetails)
        {
            if (ModelState.IsValid)
            {
                Register register = _unitOfWork.Register.GetFirstOrDefault(x => x.Username == userdetails.Username);
                if (register == null && register?.Password != userdetails.Password)
                {
                    return NotFound();
                }
                else
                {

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userdetails.Username),
                    new Claim(ClaimTypes.Role, "User"),
                };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(10),
                    };
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                    return RedirectToAction("Index", "Home", new { area = "Customer" });

                }

            }
            return View();
        }

        public IActionResult Register()
        {

            return View("Regis", new Register { role = Enum.Role.admin});
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                register.Password = HashPassword(register.Password);

                _unitOfWork.Register.Add(register);
                _unitOfWork.Save();
                return RedirectToAction("PerformLogin", "Register");
            }

            return View("PerformLogin", "Register");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("PerformLogin","Register");
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }




    }

   


}


