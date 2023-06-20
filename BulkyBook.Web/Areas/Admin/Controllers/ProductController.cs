



using Bulkybook.DataAcess.Repository;
using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> CoverType = _unitOfWork.CoverType.GetAll();
            return View(CoverType);
        }



        //GET
        public IActionResult Upsert(int? id)
        {


            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverType = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })


            };




            //IEnumerable<SelectListItem> CategotyList = _unitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            //IEnumerable<SelectListItem> CoverType = _unitOfWork.CoverType.GetAll().Select(
            //   u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()
            //   });

            if (id == null || id == 0)
            {

                return View(productVM);
            }
            else
            {
                return View(productVM);

            }


        }
        //Post
        [HttpPost]
        public IActionResult Upsert( Product obj, IFormFile file)
        {
           // _unitOfWork.CoverType.Update(coverType);
           _unitOfWork.Save();
           TempData["Sucess"] = "CoverType Edited sucessfully";
           // return RedirectToAction("Index");
            return View(obj);
        }


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
