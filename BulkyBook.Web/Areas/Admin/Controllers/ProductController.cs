



using Bulkybook.DataAcess.Repository;
using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult GenerateFile()
        {
            // Retrieve the Excel file as a byte array
            var excelData = GenerateExcelFile();

            // Set the response headers for downloading the file
            Response.Headers.Add("Content-Disposition", "attachment; filename=products.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.ContentLength = excelData.Length;

            // Write the Excel byte array directly to the response
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        [Authorize]
        //public IActionResult Index()
        //{
           
        //    return View();
        //}
        public IActionResult Index()
        {
            return View();
        }



        [Authorize]
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

            if (id == null || id == 0)
            {

                return View(productVM);
            }
            else
            {
                var data = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id.Value);
                productVM.Product = data;
                return View(productVM);


            }
            


        }
        //Post
        [HttpPost]
        public IActionResult Upsert( ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);


                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                       
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\Images\Products\" + filename + extension;
                }
                if(obj.Product.Id != 0)
                {
                    _unitOfWork.Product.Update(obj.Product);
                  
                }
                else
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                //_unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["Sucess"] = "product  Edited sucessfully";
                return RedirectToAction("Index");

            }
            
            return View(obj);
        }


        //get
        //public IActionResult Delete(int id)
        //{
        //    var cover = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //    return View(cover);


        //}
        //post
       

        #region API CAlLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return Json(new { success=false, message="object deleted" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));


            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted sucessfully" });
            //TempData["Sucess"] = "CoverType deleted sucessfully";
            return RedirectToAction("Index");   

        }
        private byte[] GenerateExcelFile()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Set header row
                var headers = new string[] { "Name", "Description", "Price", "Category", "Cover Type" };
                for (var i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                // Set data rows
                var row = 2;
                foreach (var product in productList)
                {
                    worksheet.Cells[row, 1].Value = product.Name;
                    worksheet.Cells[row, 2].Value = product.Description;
                    worksheet.Cells[row, 3].Value = product.Price;
                    worksheet.Cells[row, 4].Value = product.Category.Name;
                    worksheet.Cells[row, 5].Value = product.CoverType.Name;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }



        #endregion

    }
}
