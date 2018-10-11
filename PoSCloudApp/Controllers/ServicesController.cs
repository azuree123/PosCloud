using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using AutoMapper;
using PoSCloudApp.Core;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.ViewModels;
using WebGrease.Css.ImageAssemblyAnalysis;

namespace PoSCloudApp.Controllers
{

    [Authorize]
    public class ServicesController : Controller
    {
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
            ServiceCreateViewModel service = new ServiceCreateViewModel();
            service.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a => a.Type == "Service")
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
            Product service = Mapper.Map<Product>(serviceVm);
            _unitOfWork.ProductRepository.AddProduct(service);
            _unitOfWork.Complete();
            return RedirectToAction("ServicesList", "Services");
        }

        public ActionResult UpdateService(int id)
        {
            ViewBag.edit = "UpdateService";
            ServiceCreateViewModel serviceVm = Mapper.Map<ServiceCreateViewModel>(_unitOfWork.ProductRepository.GetProductById(id));
            serviceVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a => a.Type == "Service")
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
                _unitOfWork.ProductRepository.UpdateProduct(id, service);
                _unitOfWork.Complete();
                return RedirectToAction("ServicesList", "Services");
            }

        }
        public ActionResult DeleteService(int id)
        {
            _unitOfWork.ProductRepository.DeleteProduct(id);
            _unitOfWork.Complete();
            return RedirectToAction("ServicesList", "Services");
        }

        public ActionResult ServiceCategoryList()
        {
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories().Where(a=>a.Type=="Service"));
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
            ServiceCategoryViewModel service =
                Mapper.Map<ServiceCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
            return View("AddServiceCategory", service);
        }
        [HttpPost]
        public ActionResult UpdateServiceCategory(int id, ServiceCategoryViewModel serviceCategoryVm,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateServiceCategory";
                ServiceCategoryViewModel service =
                    Mapper.Map<ServiceCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
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
                _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id, category);
                _unitOfWork.Complete();
                return RedirectToAction("ServiceCategoryList");
            }
        }

        public ActionResult DeleteServiceCategory(int id)
        {
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id);
            _unitOfWork.Complete();
            return RedirectToAction("ServiceCategoryList", "Services");
        }
    }
}