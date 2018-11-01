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
    public class ServicesController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public ServicesController()
        {

        }
        public ServicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Services
        public ActionResult ServicesList()
        {
            return View(_unitOfWork.ProductRepository.GetAllProducts().Where(a => a.ProductCategory.Type == "Service").ToList());
        }

        public ActionResult AddService()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ServiceCreateViewModel service = new ServiceCreateViewModel();
            service.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Service")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            service.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddService";
            return View(service);
        }

        [HttpPost]
        public ActionResult AddService(ServiceCreateViewModel serviceVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddService";
            if (!ModelState.IsValid)
            {
                return View(serviceVm);
            }
            else if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Server.MapPath("~/Images/Data/Service" + file.FileName);
                        
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                        serviceVm.Image = "/Images/Data/Service" + (file.FileName);

                    }
                    else
                    {
                        file.SaveAs(path);
                        serviceVm.Image = "/Images/Data/Service" + (file.FileName);


                    }

                }
                catch (Exception exception)
                {
                    ViewBag.Message = "ERROR:" + exception.Message.ToString();
                }
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            serviceVm.StoreId = user.StoreId;
            Product service = Mapper.Map<Product>(serviceVm);
            _unitOfWork.ProductRepository.AddProduct(service);
            _unitOfWork.Complete();
            return RedirectToAction("ServicesList", "Services");
        }

        public ActionResult UpdateService(int id)
        {
            ViewBag.edit = "UpdateService";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ServiceCreateViewModel serviceVm = Mapper.Map<ServiceCreateViewModel>(_unitOfWork.ProductRepository.GetProductById(id,Convert.ToInt32(user.StoreId)));
            serviceVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Service")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            serviceVm.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddService", serviceVm);
        }
        [HttpPost]
        public ActionResult UpdateService(int id, ServiceCreateViewModel serviceVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateService";
            if (!ModelState.IsValid)
            {
                return View("AddService", serviceVm);
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string path = Server.MapPath("~/Images/Data/Service/"+ file.FileName);

                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Message = "Image Already Exists!";
                            serviceVm.Image = "/Images/Data/Service/" + (file.FileName);

                        }
                        else
                        {
                            file.SaveAs(path);
                            serviceVm.Image = "/Images/Data/Service/"+ (file.FileName);

                        }

                    }
                    catch (Exception exception)
                    {
                        ViewBag.Message = "ERROR:" + exception.Message.ToString();
                    }
                }
                Product service = Mapper.Map<Product>(serviceVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductRepository.UpdateProduct(id,Convert.ToInt32(user.StoreId) ,service);
                _unitOfWork.Complete();
                return RedirectToAction("ServicesList", "Services");
            }

        }
        public ActionResult DeleteService(int id,int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductRepository.DeleteProduct(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ServicesList", "Services");
        }
        public ActionResult AddServiceCategoryPartial()
        {
            ViewBag.edit = "AddServiceCategoryPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddServiceCategoryPartial(ServiceCategoryViewModel productcategoryvm)
        {
            ViewBag.edit = "AddServiceCategoryPartial";
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
                productcategory.Type = "Service";
                _unitOfWork.ProductCategoryRepository.AddProductCategory(productcategory);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }
        public ActionResult ServiceCategoryList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a=>a.Type=="Service"));
        }

        public ActionResult AddServiceCategory()
        {
            ViewBag.edit = "AddServiceCategory";
            return View();
        }
        [HttpPost]
        public ActionResult AddServiceCategory(ServiceCategoryViewModel serviceCategory,HttpPostedFileBase file)
        {
            ViewBag.edit = "AddServiceCategory";
            if (!ModelState.IsValid)
            {
                return View(serviceCategory);
            }
            else
            {
                if (file !=null && file.ContentLength>0)
                {
                    try
                    {
                        string path = Server.MapPath("~/Images/Data/Service/" + file.FileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Message = "Image Already Exist!";
                            serviceCategory.Image = "/Images/Data/Service/" + (file.FileName);

                        }
                        else
                        {
                            file.SaveAs(path);
                            serviceCategory.Image = "/Images/Data/Service/" + (file.FileName);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }
                }
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                serviceCategory.StoreId = user.StoreId;
                serviceCategory.Type = "Service";
                ProductCategory category = Mapper.Map<ProductCategory>(serviceCategory);
                _unitOfWork.ProductCategoryRepository.AddProductCategory(category);
                _unitOfWork.Complete();
                return RedirectToAction("ServiceCategoryList");
            }
        }
        public ActionResult UpdateServiceCategory(int id)
        {
            ViewBag.edit = "UpdateServiceCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ServiceCategoryViewModel service =
                Mapper.Map<ServiceCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
            return View("AddServiceCategory", service);
        }
        [HttpPost]
        public ActionResult UpdateServiceCategory(int id, ServiceCategoryViewModel serviceCategoryVm,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateServiceCategory";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ServiceCategoryViewModel service =
                    Mapper.Map<ServiceCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
                return View("AddServiceCategory", service);
            }
            else if (file != null && file.ContentLength>0)
            {
                try
                {
                    string path = Server.MapPath("~/Images/Data/Service/" + file.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                        serviceCategoryVm.Image = "/Images/Data/Service/" + (file.FileName);

                    }
                    else
                    {
                        file.SaveAs(path);
                        serviceCategoryVm.Image = "/Images/Data/Service/" + (file.FileName);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
                
            }
            {
                serviceCategoryVm.Type = "Service";
                ProductCategory category  = Mapper.Map<ProductCategory>(serviceCategoryVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id, Convert.ToInt32(user.StoreId) ,category);
                _unitOfWork.Complete();
                return RedirectToAction("ServiceCategoryList");
            }
        }

        public ActionResult DeleteServiceCategory(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ServiceCategoryList", "Services");
        }
        public JsonResult GetServiceCategoryDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ServiceCategoryViewModel[]>(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a=>a.Type=="Service")), JsonRequestBehavior.AllowGet);
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