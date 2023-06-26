using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    

    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Regis()
        {

            return View("Regis");
        }
        [HttpPost]
        public IActionResult Regis(Register register)
        {

            if (register == null)
            {

                return View("Index");
            }
            else
            {
                var user = _db.Registers.Add(register);
                _db.SaveChanges();
                return View("Index");
            }

        }
    }
}
