



using Bulkybook.DataAcess.Repository;
using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<CoverType> CoverType = _unitOfWork.CoverType.GetAll();
            return View(CoverType);
        }
        [Authorize]
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        public IActionResult Create(CoverType coverType)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["Sucess"] = "CoverType Created sucessfully";
                return RedirectToAction("Index");


            }
            return View(coverType);

        }
        [Authorize]
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {

                return NotFound();
            }
            var cover = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (cover == null)
            {
                return NotFound();
            }
            return View(cover);
        }
        //Post
        [HttpPost]
        public IActionResult Edit(CoverType coverType)
        {
            _unitOfWork.CoverType.Update(coverType);
            _unitOfWork.Save();
            TempData["Sucess"] = "CoverType Edited sucessfully";
            return RedirectToAction("Index");
        }

        [Authorize]
        //get
        public IActionResult Delete(int id)
        {
            var cover = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            return View(cover);


        }
        //post
        [HttpPost]
        public IActionResult Delete(CoverType coverType)
        {
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["Sucess"] = "CoverType deleted sucessfully";
            return RedirectToAction("Index");

        }

    }
}
