using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
            return View(objProductList);
        }

        



        //GET
        public IActionResult Upsert(int? id)
        {
            Product product = new();

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );

            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );

            if (id == null || id == 0)
            {
                //Create Product
                ViewBag.CategoryList = CategoryList;
                return View(product);
            }
            else
            {
                //Update Product
            }
           
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType obj)
        {
            //Server side validation
            //if (obj.Name == obj.Name.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot be exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type successfully Updated!";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }


            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type successfully deleted!";
            return RedirectToAction("Index");


        }




    }
}
