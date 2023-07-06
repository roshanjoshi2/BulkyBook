



using Bulkybook.DataAcess.Repository;
using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Category> categorylist = _unitOfWork.Category.GetAll();
            return View(categorylist);
        }
        [Authorize]
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["Sucess"] = "Üser Created sucessfully";
                return RedirectToAction("Index");


            }
            return View(category);

        }
        [Authorize]
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Post
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["Sucess"] = "Üser Edited sucessfully";
            return RedirectToAction("Index");
        }

        [Authorize]
        //get
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            return View(category);


        }
        //post
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["Sucess"] = "Üser deleted sucessfully";
            return RedirectToAction("Index");

        }

    }
}
