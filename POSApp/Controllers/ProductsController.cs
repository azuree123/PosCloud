using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public ProductsController()
        {
            
        }
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Products
        public ActionResult ProductsList()
        {
            return View(_unitOfWork.ProductRepository.GetAllProducts().Where(a=>a.ProductCategory.Type=="Product").ToList());
        }

        public ActionResult AddProduct()
        {
            ProductCreateViewModel product = new ProductCreateViewModel();
            product.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a=>a.Type=="Product")
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            product.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            ViewBag.edit = "AddProduct";
           
            return View(product);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductCreateViewModel productVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddProduct";
            if (!ModelState.IsValid)
            {
                productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a => a.Type == "Product")
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                productVm.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View(productVm);
            }
            else 
            if (file !=null && file.ContentLength > 0)
            {
                
                try
                {
                    string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productVm.Image = "/Images/Data/Product/" + file.FileName;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
                
            }
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productVm.StoreId = user.StoreId;
                Product product = Mapper.Map<Product>(productVm);
                _unitOfWork.ProductRepository.AddProduct(product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }
            
        }
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.edit = "UpdateProduct";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel productVm = Mapper.Map<ProductCreateViewModel>(_unitOfWork.ProductRepository.GetProductById(id,Convert.ToInt32(user.StoreId)));
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a => a.Type == "Product")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddProduct", productVm);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id, ProductCreateViewModel productVm,HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateProduct";
            if (!ModelState.IsValid)
            {
                return View("AddProduct", productVm);
            }
            else if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productVm.Image = "/Images/Data/Product/" + file.FileName;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
            }

            {
                Product product = Mapper.Map<Product>(productVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductRepository.UpdateProduct(id,Convert.ToInt32(user.StoreId),product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }

        }
        public ActionResult DeleteProduct(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductRepository.DeleteProduct(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ProductsList", "Products");
        }

        public ActionResult ProductCategoryList()
        {
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories());
        }




        public ActionResult AddProductCategoryPartial()
        {
            ViewBag.edit = "AddProductCategoryPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryPartial(ProductCategoryViewModel productcategoryvm)
        {
            ViewBag.edit = "AddProductCategoryPartial";
            if (!ModelState.IsValid)
            {
                return View(productcategoryvm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productcategoryvm.StoreId = user.StoreId;
                ProductCategory productcategory = Mapper.Map<ProductCategory>(productcategoryvm);
                productcategory.Type = "Product";
                _unitOfWork.ProductCategoryRepository.AddProductCategory(productcategory);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }

        public ActionResult AddProductCategory()
        {
            ViewBag.edit = "AddProductCategory";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryViewModel productCategory, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddProductCategory";
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productCategory.Image = "/Images/Data/Product/" + file.FileName;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productCategory.StoreId = user.StoreId;
                productCategory.Type = "Product";
                ProductCategory category = Mapper.Map<ProductCategory>(productCategory);
                _unitOfWork.ProductCategoryRepository.AddProductCategory(category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }
        public ActionResult UpdateProductCategory(int id)
        {
            ViewBag.edit = "UpdateProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCategoryViewModel product =
                Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
            return View("AddProductCategory", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory(int id, ProductCategoryViewModel productCategoryVm, HttpPostedFileBase file)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateProductCategory";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategoryViewModel product =
                    Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
                return View("AddProductCategory", product);
            }
            else
            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productCategoryVm.Image = "/Images/Data/Product/" + file.FileName;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
                productCategoryVm.Type = "Product";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategory category = Mapper.Map<ProductCategory>(productCategoryVm);
                _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id,Convert.ToInt32(user.StoreId),category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }

        public ActionResult DeleteProductCategory(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id,Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ProductCategoryList", "Products");
        }

        public JsonResult GetProductCategoryDdl()
        {
            try
            {
                return Json(Mapper.Map<ProductCategoryViewModel[]>(_unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a => a.Type == "Product")), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}