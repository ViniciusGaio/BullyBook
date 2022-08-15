using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using BullyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BullyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostWnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostWnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostWnvironment = hostWnvironment;
        }

        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.CoverType.GetAll();
            return View(objCategoryList);
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
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            
            //create product
            if (id == null || id == 0) 
            {
                //ViewBag.CategoryList = CategoryList;
               // ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM); 
            }
            //Update Product
            else
            {

            }
            
            return View(productVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostWnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products" + fileName + extension;
                }
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDbFirst == null) return NotFound();

            return View(CoverTypeFromDbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null) return NotFound();

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");


        }
    }
}

