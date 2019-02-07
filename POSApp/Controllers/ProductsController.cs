﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.Shared;
using POSApp.Core.ViewModels;
using System.Linq.Dynamic;

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
          
            return View();
        }
        [HttpPost]
        public ActionResult GetProductsData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v = _unitOfWork.ProductRepository.GetProductsQuery((int)user.StoreId).Where(a => a.Type != "Combo");
                
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.ProductCode.Contains(searchColumn) || a.Name.Contains(searchColumn) || a.Size.Contains(searchColumn));
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //Product Partial
        public ActionResult AddProductPartial()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel product = new ProductCreateViewModel();
            product.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type != "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            product.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
            product.ProductCode = "PRO-" + "C-" + prodId.ToString() + "-" + user.StoreId;
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductsList");
            }
            ViewBag.edit = "AddProductPartial";

            return View(product);
        }
        [HttpPost]
        public ActionResult AddProductPartial(ProductCreateViewModel productVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddProductPartial";
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                if (file != null && file.ContentLength > 0)
                {

                    try
                    {
                        //string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                        //if (System.IO.File.Exists(path))
                        //{
                        //    ViewBag.Message = "Image Already Exists!";
                        //}
                        //else
                        //{
                        //    file.SaveAs(path);
                        //}
                        productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productVm.Image, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                }
                {

                    productVm.StoreId = user.StoreId;
                    Product product = Mapper.Map<Product>(productVm);
                    int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
                    product.ProductCode = "PRO-" + "C-" + prodId.ToString() + "-" + user.StoreId;
                    product.Type = "Product";
                    _unitOfWork.ProductRepository.AddProduct(product);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product added successfully", AlertType.Success);
                    return PartialView("Test");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return PartialView("Test");


        }
        public JsonResult GetProductDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ProductCreateViewModel[]>(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //Add Product
        public ActionResult AddProduct()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel product = new ProductCreateViewModel();
            product.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a=>a.Type!="Combo")
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            product.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            product.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
            product.ProductCode =  "PRO-" + "C-" + prodId.ToString() + "-" + user.StoreId;
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductsList");
            }
            ViewBag.edit = "AddProduct";
           
            return View(product);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductCreateViewModel productVm, HttpPostedFileBase file)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddProduct";
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type != "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(productVm);
                }
                else
                if (file != null && file.ContentLength > 0)
                {

                    try
                    {
                        //string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                        //if (System.IO.File.Exists(path))
                        //{
                        //    ViewBag.Message = "Image Already Exists!";
                        //}
                        //else
                        //{
                        //    file.SaveAs(path);
                        //}
                        productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productVm.Image, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                }
                {

                    productVm.StoreId = user.StoreId;
                    Product product = Mapper.Map<Product>(productVm);
                    int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
                    product.ProductCode = "PRO-" + "C-" + prodId.ToString() + "-" + user.StoreId;
                    product.Type = "Product";
                    _unitOfWork.ProductRepository.AddProduct(product);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product added successfully", AlertType.Success);
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View(productVm);


        }
        public ActionResult UpdateProduct(string productId)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductsList");
            }
            ViewBag.edit = "UpdateProduct";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel productVm = Mapper.Map<ProductCreateViewModel>(_unitOfWork.ProductRepository.GetProductByCode(productId, Convert.ToInt32(user.StoreId)));
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type != "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddProduct", productVm);
        }
        [HttpPost]
        public ActionResult UpdateProduct(string productId, ProductCreateViewModel productVm,HttpPostedFileBase file)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateProduct";
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type != "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddProduct",productVm);
                }
                else 
                {
                    if (file != null && file.ContentLength > 0)
                        try
                    {
                        productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productVm.Image, 0, file.ContentLength);

                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                    Product product = Mapper.Map<Product>(productVm);
                    product.Type = "Product";
                    _unitOfWork.ProductRepository.UpdateProduct(product.ProductCode, Convert.ToInt32(user.StoreId), product);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product updated successfully", AlertType.Success);
                    return null;
                }
                
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddProduct",productVm);


        }
        public ActionResult DeleteProduct(string id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductRepository.DeleteProduct(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The product deleted successfully", AlertType.Success);
                return RedirectToAction("ProductsList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                {
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                    }

            }

            return RedirectToAction("ProductsList", "Products");

        }

        public ActionResult ProductCategoryList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId));
        }
        public ActionResult AddProductCategoryPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryList");
            }
            ViewBag.edit = "AddProductCategoryPartial";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryPartial(ProductCategoryViewModel productcategoryvm)
        {
           
            ViewBag.edit = "AddProductCategoryPartial";
            if (!ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Name
                });
                return View(productcategoryvm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productcategoryvm.StoreId = user.StoreId;
                ProductCategory productcategory = Mapper.Map<ProductCategory>(productcategoryvm);
                _unitOfWork.ProductCategoryRepository.AddProductCategory(productcategory);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }

        public ActionResult AddProductCategory()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryList");
            }
            ViewBag.edit = "AddProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int) user.StoreId).Select(a=> new SelectListItem
            {
                Text = a.Name,Value = a.Name
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryViewModel productCategory, HttpPostedFileBase file)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddProductCategory";
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(productCategory);
                }
                else
                if (file != null && file.ContentLength > 0)
                {

                    try
                    {
                        productCategory.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productCategory.Image, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                }
                {

                    productCategory.StoreId = user.StoreId;
                    ProductCategory category = Mapper.Map<ProductCategory>(productCategory);
                    _unitOfWork.ProductCategoryRepository.AddProductCategory(category);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product category added successfully", AlertType.Success);
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }

                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View(productCategory);

        }
        public ActionResult UpdateProductCategory(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryList");
            }
            ViewBag.edit = "UpdateProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCategoryViewModel product =
                Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
       
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            return View("AddProductCategory", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory(int id, ProductCategoryViewModel productCategoryVm, HttpPostedFileBase file)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateProductCategory";
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddProductCategory", productCategoryVm);
                }
                else
                if (file != null && file.ContentLength > 0)
                {

                    try
                    {
                        productCategoryVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productCategoryVm.Image, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                }
                {
                    
                    ProductCategory category = Mapper.Map<ProductCategory>(productCategoryVm);
                    _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id, Convert.ToInt32(user.StoreId), category);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product category updated successfully", AlertType.Success);
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddProductCategory",productCategoryVm);


        }

        public ActionResult DeleteProductCategory(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The product category deleted successfully", AlertType.Success);
                return RedirectToAction("ProductCategoryList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ProductCategoryList", "Products");


        }

        public JsonResult GetProductCategoryDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ProductCategoryDdlViewModel[]>(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult ProductCategoryGroupList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId));
        }
        public ActionResult AddProductCategoryGroupPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryGroupList");
            }
            ViewBag.edit = "AddProductCategoryGroupPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryGroupPartial(ProductCategoryGroupViewModel ProductCategoryGroupvm)
        {
            ViewBag.edit = "AddProductCategoryGroupPartial";
            if (!ModelState.IsValid)
            {
                return View(ProductCategoryGroupvm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategoryGroupvm.StoreId = (int)user.StoreId;
                ProductCategoryGroup ProductCategoryGroup = Mapper.Map<ProductCategoryGroup>(ProductCategoryGroupvm);
               
                _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(ProductCategoryGroup);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }

        public ActionResult AddProductCategoryGroup()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryGroupList");
            }
            ViewBag.edit = "AddProductCategoryGroup";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryGroup(ProductCategoryGroupViewModel productCategoryGroup)
        {
            ViewBag.edit = "AddProductCategoryGroup";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(productCategoryGroup);
                }
                else

                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    productCategoryGroup.StoreId = (int)user.StoreId;

                    ProductCategoryGroup category = Mapper.Map<ProductCategoryGroup>(productCategoryGroup);
                    _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(category);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product category group added successfully", AlertType.Success);
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View(productCategoryGroup);

        }
        public ActionResult UpdateProductCategoryGroup(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ProductCategoryGroupList");
            }
            ViewBag.edit = "UpdateProductCategoryGroup";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCategoryGroupViewModel product =
                Mapper.Map<ProductCategoryGroupViewModel>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroup(id,(int)user.StoreId));
            return View("AddProductCategoryGroup", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategoryGroup(int id, ProductCategoryGroupViewModel productCategoryGroupVm)
        {
            ViewBag.edit = "UpdateProductCategoryGroup";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddProductCategoryGroup",productCategoryGroupVm);
                }
                else
                {

                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    ProductCategoryGroup category = Mapper.Map<ProductCategoryGroup>(productCategoryGroupVm);
                    _unitOfWork.ProductCategoryGroupRepository.UpdateProductCategoryGroup(id, Convert.ToInt32(user.StoreId), category);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The product category group updated successfully", AlertType.Success);
                    return null;
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddProductCategoryGroup", productCategoryGroupVm);


        }

        public ActionResult DeleteProductCategoryGroup(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductCategoryGroupRepository.DeleteProductCategoryGroup(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The product category group deleted successfully", AlertType.Success);
                return RedirectToAction("ProductCategoryGroupList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ProductCategoryGroupList", "Products");

        }

        //ModifierOption
        public ActionResult ModifierOptionList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ModifierOptionRepository.GetModifierOptions((int)user.StoreId).ToList());
        }

        public ActionResult AddModifierOption()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierOptionViewModel modifieroption = new ModifierOptionViewModel();

            ViewBag.edit = "AddModifierOption";
           
            return View(modifieroption);
        }
        [HttpPost]
        public ActionResult AddModifierOption(ModifierOptionViewModel modifieroptionVm)
        {
            if (Helper.TempModifierOptions == null)
            {
                Helper.TempModifierOptions = new List<ModifierOptionViewModel>();
            }
            ViewBag.edit = "AddModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                return View(modifieroptionVm);
            }
            else
            
            {
                modifieroptionVm.StoreId = user.StoreId;
                Helper.AddToTempModifierOptions(modifieroptionVm,userid);
                return View("ModifierOptionListTable", Helper.TempModifierOptions.Where(a => a.CreatedBy == userid).ToList());
            }

        }
        public ActionResult UpdateModifierOption(string name)
        {
            
            ViewBag.edit = "AddModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierOptionViewModel modifieroptionVm = Helper.TempModifierOptions.FirstOrDefault(a=>a.Name==name && a.CreatedBy==userid);
            ViewBag.com = "<script>$('#nameDis').attr('readonly', true);</script>";
            return View("AddModifierOption", modifieroptionVm);
        }
        [HttpPost]
        public ActionResult UpdateModifierOption(int id, ModifierOptionViewModel modifieroptionVm)
        {
            ViewBag.edit = "UpdateModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                return View("AddModifierOption", modifieroptionVm);
            }
            else 
            

            {
                
                ModifierOption modifierOption = Mapper.Map<ModifierOption>(modifieroptionVm);
                _unitOfWork.ModifierOptionRepository.UpdateModifierOptions(id, Convert.ToInt32(user.StoreId), modifierOption);
                _unitOfWork.Complete();
                

                return RedirectToAction("ModifierOptionList", "Products");
            }

        }
        public ActionResult DeleteModifierOption(string name, int storeid)
        {
            if (Helper.TempModifierOptions == null)
            {
                Helper.TempModifierOptions = new List<ModifierOptionViewModel>();
                return View("ModifierOptionListTable", Helper.TempModifierOptions.ToList());

            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            Helper.RemoveFromTempModifierOptions(name,(int)user.StoreId,userid);
            //_unitOfWork.Complete();
            return View("ModifierOptionListTable", Helper.TempModifierOptions.Where(a => a.CreatedBy == userid).ToList());

        }
        public JsonResult GetProductCategoryGroupDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ProductCategoryGroupViewModel[]>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ActionResult ModifierList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<ModifierListViewModel[]>(_unitOfWork.ModifierRepository.GetModifiers((int)user.StoreId)));
        }
        [HttpGet]
        public ActionResult AddModifier()
        {
        
            ViewBag.edit = "AddModifier";
            
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (Helper.TempModifierOptions != null)
            {

                Helper.EmptyTempModifierOptions(user.Id, (int)user.StoreId);
            }
            return View(new ModifierViewModel());
        }
        [HttpPost]
        public ActionResult AddModifier(ModifierViewModel modifierVm)
        {
            ViewBag.edit = "AddModifier";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    Modifier modifier = Mapper.Map<Modifier>(modifierVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    modifier.StoreId = (int)user.StoreId;
                    var mod = modifier;
                    _unitOfWork.ModifierRepository.AddModifier(mod);
                    _unitOfWork.Complete();
                    foreach (var modifierOptionViewModel in Helper.TempModifierOptions.Where(a => a.CreatedBy == userid))
                    {
                        modifierOptionViewModel.ModifierId = mod.Id;
                        _unitOfWork.ModifierOptionRepository.AddModifierOption(Mapper.Map<ModifierOption>(modifierOptionViewModel));
                    }
                    _unitOfWork.Complete();
                    Helper.EmptyTempModifierOptions(user.Id, (int)user.StoreId);
                    TempData["Alert"] = new AlertModel("The modifier added successfully", AlertType.Success);
                    return RedirectToAction("ModifierList", "Products");
                }

            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ModifierList", "Products");

        }
        [HttpGet]
        public ActionResult UpdateModifier(int id)
        {
         
            ViewBag.edit = "UpdateModifier";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (Helper.TempModifierOptions == null)
            {
                Helper.TempModifierOptions = new List<ModifierOptionViewModel>();

            }
            if (Helper.TempModifierOptions != null)
            {

                Helper.EmptyTempModifierOptions(user.Id, (int)user.StoreId);
            }
            ModifierViewModel modifierVm =
                Mapper.Map<ModifierViewModel>(_unitOfWork.ModifierRepository.GetModifierById(id, (int)user.StoreId));
            foreach (var modifierVmModifierOptionViewModel in modifierVm.ModifierOptionViewModels)
            {
                Helper.AddToTempModifierOptions(modifierVmModifierOptionViewModel,userid);
            }

            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddModifier", modifierVm);
        }
        [HttpPost]
        public ActionResult UpdateModifier(int id, ModifierViewModel modifierVm)
        {
            ViewBag.edit = "UpdateModifier";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    Modifier modifier = Mapper.Map<Modifier>(modifierVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    modifier.StoreId = (int)user.StoreId;
                    _unitOfWork.ModifierRepository.UpdateModifier(id, modifier.StoreId, modifier);
                    _unitOfWork.Complete();
                    _unitOfWork.ModifierOptionRepository.DeleteModifierOptionsByModifierId(modifier.Id, modifier.StoreId);
                    foreach (var modifierOptionViewModel in Helper.TempModifierOptions.Where(a => a.CreatedBy == userid))
                    {
                        modifierOptionViewModel.ModifierId = modifier.Id;
                        _unitOfWork.ModifierOptionRepository.AddModifierOption(Mapper.Map<ModifierOption>(modifierOptionViewModel));
                    }
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The modifier updated successfully", AlertType.Success);
                    return RedirectToAction("ModifierList", "Products");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ModifierList", "Products");


        }
        public ActionResult DeleteModifier(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ModifierRepository.DeleteModifier(id, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("ModifierList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ModifierList", "Products");


        }

        //Modifier Link Product
        //public ActionResult ModifierLinkProductList()
        //{
        //    var userid = User.Identity.GetUserId();
        //    var user = UserManager.FindById(userid);
        //    return View(_unitOfWork.ModifierLinkProductRepository.GetModifierLinkProducts());
        //}
        [HttpGet]
        public ActionResult AddModifierLinkProduct(int modifierId)
        {
            ModifierLinkProductViewModel ModifierLinkProduct = new ModifierLinkProductViewModel();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierLinkProduct.ProductDDl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name }).AsEnumerable();
            ModifierLinkProduct.ModifierId = modifierId;
            if (_unitOfWork.ModifierLinkProductRepository.GetModifierLinkProducts(modifierId, (int) user.StoreId)
                .Select(a => a.ProductCode).Any())
            {
                ModifierLinkProduct.ProductsDisplay = string.Join(",",
                    _unitOfWork.ModifierLinkProductRepository.GetModifierLinkProducts(modifierId, (int) user.StoreId)
                        .Select(a => a.ProductCode));
            }
            
            ViewBag.edit = "AddModifierLinkProduct";
            return View(ModifierLinkProduct);
        }
        [HttpPost]
        public ActionResult AddModifierLinkProduct(ModifierLinkProductViewModel ModifierLinkProductMv)
        {

            ViewBag.edit = "AddModifierLinkProduct";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    if (_unitOfWork.ModifierLinkProductRepository.GetModifierLinkProducts(ModifierLinkProductMv.ModifierId, (int)user.StoreId)
                        .Select(a => a.ProductCode).Any())
                    {
                        _unitOfWork.ModifierLinkProductRepository.DeleteModifierLinkProducts(
                            ModifierLinkProductMv.ModifierId, (int) user.StoreId);
                        _unitOfWork.Complete();
                    }
                    
                    foreach (var product in ModifierLinkProductMv.Products)
                    {
                    ModifierLinkProduct ModifierLinkProduct = Mapper.Map<ModifierLinkProduct>(ModifierLinkProductMv);
                        ModifierLinkProduct.ProductCode = product;
                        ModifierLinkProduct.ModifierStoreId = (int) user.StoreId;
                        ModifierLinkProduct.ProductStoreId = (int) user.StoreId;
                    _unitOfWork.ModifierLinkProductRepository.AddModifierLinkProducts(ModifierLinkProduct);
                    }
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The modifierLinkProduct added successfully", AlertType.Success);
                    return RedirectToAction("ModifierList", "Products");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ModifierList", "Products");


        }
        //[HttpGet]
        //public ActionResult UpdateModifierLinkProduct(string productCode, int modifierId)
        //{
        //    ViewBag.edit = "UpdateModifierLinkProduct";
        //    var userid = User.Identity.GetUserId();
        //    var user = UserManager.FindById(userid);
        //    ModifierLinkProductViewModel ModifierLinkProductMv =
        //        Mapper.Map<ModifierLinkProductViewModel>(_unitOfWork.ModifierLinkProductRepository.GetModifierLinkProductById(productCode,modifierId));
        //    return View("AddModifierLinkProduct", ModifierLinkProductMv);
        //}
        //[HttpPost]
        //public ActionResult UpdateModifierLinkProduct(int modifierId, ModifierLinkProductViewModel ModifierLinkProductMv,string productCode)
        //{
        //    ViewBag.edit = "UpdateModifierLinkProduct";
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var message = string.Join(" | ", ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage));
        //            TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
        //        }
        //        else
        //        {
        //            var userid = User.Identity.GetUserId();
        //            var user = UserManager.FindById(userid);
        //            ModifierLinkProduct ModifierLinkProduct = Mapper.Map<ModifierLinkProduct>(ModifierLinkProductMv);
        //            _unitOfWork.ModifierLinkProductRepository.UpdateModifierLinkProducts(productCode, modifierId, ModifierLinkProduct);
        //            _unitOfWork.Complete();
        //            TempData["Alert"] = new AlertModel("The modifierLinkProduct updated successfully", AlertType.Success);
        //            return RedirectToAction("ModifierLinkProductList", "Products");

        //        }
        //    }
        //    catch (DbEntityValidationException ex)
        //    {

        //        foreach (var entityValidationError in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in entityValidationError.ValidationErrors)
        //            {
        //                TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

        //            }
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
        //        if (e.InnerException != null)
        //            if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
        //            {
        //                if (e.InnerException.InnerException != null)
        //                    if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
        //                    {
        //                        TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
        //                    }
        //            }
        //            else
        //            {
        //                TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
        //            }
        //        else
        //        {
        //            TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
        //        }
        //    }

        //    return RedirectToAction("ModifierLinkProductList", "Products");



        //}
        //public ActionResult DeleteModifierLinkProduct(int id)
        //{
        //    try
        //    {
        //        var userid = User.Identity.GetUserId();
        //        var user = UserManager.FindById(userid);
        //        _unitOfWork.ModifierLinkProductRepository.DeleteModifierLinkProducts(id);
        //        _unitOfWork.Complete();
        //        TempData["Alert"] = new AlertModel("The modifierLinkProduct deleted successfully", AlertType.Success);
        //        return RedirectToAction("ModifierLinkProductList", "Products");
        //    }
        //    catch (DbEntityValidationException ex)
        //    {

        //        foreach (var entityValidationError in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in entityValidationError.ValidationErrors)
        //            {
        //                TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

        //            }
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
        //        if (e.InnerException != null)
        //            if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
        //            {
        //                if (e.InnerException.InnerException != null)
        //                    if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
        //                    {
        //                        TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
        //                    }
        //            }
        //            else
        //            {

        //                TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
        //            }
        //        else
        //        {
        //            TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
        //        }
        //    }

        //    return RedirectToAction("ModifierLinkProductList", "Products");


        //}
        //Combos
        public ActionResult CombosList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.Type == "Combo").ToList());
        }

        public ActionResult AddCombo()
        {
            
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ComboViewModel product = new ComboViewModel();
            if (Helper.TempComboOptions == null)
            {
                Helper.TempComboOptions = new List<ProductSubViewModel>();

            }
            if (Helper.TempComboOptions != null)
            {

                Helper.EmptyTempComboOptions(user.Id, (int)user.StoreId);
            }
            product.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
           
            product.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            product.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddCombo";
            int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
         
            product.ProductCode = "PRO-" + "C-" + prodId.ToString() + "-" + user.StoreId;

            return View(product);
        }
        [HttpPost]
        public ActionResult AddCombo(ComboViewModel productVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddCombo";
            try
            {
                
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(productVm);
                }
                else
                if (file != null && file.ContentLength > 0)
                {

                    try
                    {

                        productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productVm.Image, 0, file.ContentLength);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }

                }
                {

                    productVm.StoreId = user.StoreId;
                    var product = Mapper.Map<Product>(productVm);
                    product.Type = "Combo";
                    _unitOfWork.ProductRepository.AddProduct(product);
                    int prodId = _unitOfWork.AppCountersRepository.GetId("Product");
                    
                    foreach (var productSubViewModel in Helper.TempComboOptions.Where(a=>a.CreatedBy==userid))
                    {
                        productSubViewModel.StoreId = product.StoreId;
                        productSubViewModel.ComboProductCode = product.ProductCode;
                        var subProduct = Mapper.Map<ProductsSub>(productSubViewModel);
                        _unitOfWork.ProductsSubRepository.AddProductsSub(subProduct);

                    }
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The combo added successfully", AlertType.Success);
                    return RedirectToAction("CombosList", "Products");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View(productVm);



        }
        public ActionResult UpdateCombo(int id)
        {
          
            ViewBag.edit = "UpdateCombo";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (Helper.TempComboOptions == null)
            {
                Helper.TempComboOptions = new List<ProductSubViewModel>();

            }
            if (Helper.TempComboOptions != null)
            {

                Helper.EmptyTempComboOptions(user.Id, (int)user.StoreId);
            }
            ComboViewModel productVm = Mapper.Map<ComboViewModel>(_unitOfWork.ProductRepository.GetProductById(id, Convert.ToInt32(user.StoreId)));
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Combo")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
           
            productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SizeDdl = _unitOfWork.SizeRepository.GetSizes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name }).AsEnumerable();
            productVm.ProductSubViewModels = Mapper.Map<ProductSubViewModel[]>(
                _unitOfWork.ProductsSubRepository.GetProductsSubs(productVm.ProductCode, (int) productVm.StoreId));
            foreach (var modifierVmModifierOptionViewModel in productVm.ProductSubViewModels)
            {
                modifierVmModifierOptionViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(modifierVmModifierOptionViewModel.ProductCode,
                        modifierVmModifierOptionViewModel.StoreId).Name;
                Helper.AddToTempComboOptions(modifierVmModifierOptionViewModel, userid);
            }

            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddCombo", productVm);
        }
        [HttpPost]
        public ActionResult UpdateCombo(string id, ComboViewModel productVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateCombo";
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddCombo", productVm);

                }
                else if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                        file.InputStream.Read(productVm.Image, 0, file.ContentLength);

                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = "ERROR:" + e.Message.ToString();
                    }
                }

                {
                    Product product = Mapper.Map<Product>(productVm);
                    product.Type = "Combo";
                    _unitOfWork.ProductRepository.UpdateProduct(id, Convert.ToInt32(user.StoreId), product);
                    _unitOfWork.Complete();
                    List<ProductsSub> productsSubs = _unitOfWork.ProductsSubRepository.GetProductsSubs(product.ProductCode,product.StoreId).ToList();
                    foreach (var productsSub in productsSubs)
                    {
                        _unitOfWork.ProductsSubRepository.DeleteProductsSub(productsSub.ProductCode,productsSub.ComboProductCode,productsSub.StoreId);
                    }
                    foreach (var productSubViewModel in Helper.TempComboOptions.Where(a => a.CreatedBy == userid))
                    {
                        productSubViewModel.StoreId = product.StoreId;
                        productSubViewModel.ComboProductCode = product.ProductCode;
                        _unitOfWork.ProductsSubRepository.AddProductsSub(Mapper.Map<ProductsSub>(productSubViewModel));

                    }
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The combo updated successfully", AlertType.Success);
                    return RedirectToAction("CombosList", "Products");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddCombo", productVm);



        }
        public ActionResult DeleteCombo(string id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ProductRepository.DeleteProduct(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The combo added successfully", AlertType.Success);
                return RedirectToAction("CombosList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("CombosList", "Products");

        }
        public ActionResult AddComboOption()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductSubViewModel Combooption = new ProductSubViewModel();
            
            ViewBag.edit = "AddComboOption";
            Combooption.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int) user.StoreId)
                .Select(a => new SelectListItem {Text = a.Name, Value = a.ProductCode.ToString()}).ToList();
            return View(Combooption);
        }
        [HttpPost]
        public ActionResult AddComboOption(ProductSubViewModel CombooptionVm)
        {
            if (Helper.TempComboOptions == null)
            {
                Helper.TempComboOptions = new List<ProductSubViewModel>();
            }
            ViewBag.edit = "AddComboOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                CombooptionVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                    .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode.ToString() }).ToList();
                return View(CombooptionVm);
            }
            else

            {
                CombooptionVm.StoreId = (int)user.StoreId;
                Helper.AddToTempComboOptions(CombooptionVm, userid);
                //ComboOption Combooption = Mapper.Map<ComboOption>(CombooptionVm);
                //_unitOfWork.ComboOptionRepository.AddComboOption(Combooption);
                //_unitOfWork.Complete();
                return View("ComboOptionListTable", Helper.TempComboOptions.Where(a => a.CreatedBy == userid).ToList());
            }

        }
        public ActionResult UpdateComboOption(string productId,int storeId)
        {
            
            ViewBag.edit = "AddComboOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductSubViewModel CombooptionVm = Helper.TempComboOptions.FirstOrDefault(a => a.ProductCode == productId && a.CreatedBy == userid);
            CombooptionVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode }).ToList();
            ViewBag.com = "<script>$('#productCode').css('pointer-events','none');</script>";
            return View("AddComboOption", CombooptionVm);
        }
        
        public ActionResult DeleteComboOption(string productId, int storeid)
        {
            if (Helper.TempComboOptions == null)
            {
                Helper.TempComboOptions = new List<ProductSubViewModel>();
                return View("ComboOptionListTable", Helper.TempComboOptions.ToList());

            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            Helper.RemoveFromTempComboOptions(productId, (int)user.StoreId, userid);
            //_unitOfWork.ComboOptionRepository.DeleteComboOptions(id, Convert.ToInt32(user.StoreId));
            //_unitOfWork.Complete();
            return View("ComboOptionListTable", Helper.TempComboOptions.Where(a => a.CreatedBy == userid).ToList());

        }
        //Section
        public ActionResult AddSectionPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SectionList");
            }
            ViewBag.edit = "AddSectionPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddSectionPartial(SectionViewModel sectionVm)
        {
            ViewBag.edit = "AddSectionPartial";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {

                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    sectionVm.StoreId = (int)user.StoreId;
                    Section section = Mapper.Map<Section>(sectionVm);
                    _unitOfWork.SectionRepository.AddSection(section);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The section added successfully", AlertType.Success);
                    return PartialView("Test");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return PartialView("Test");


        }

        public JsonResult GetSectionDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<SectionViewModel[]>(_unitOfWork.SectionRepository.GetSections((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ActionResult SectionList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.SectionRepository.GetSections((int)user.StoreId));
        }
        [HttpGet]
        public ActionResult AddSection()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SectionList");
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SectionViewModel Section = new SectionViewModel();
            ViewBag.edit = "AddSection";
            return View(Section);
        }
        [HttpPost]
        public ActionResult AddSection(SectionViewModel SectionMv)
        {

            ViewBag.edit = "AddSection";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Section Section = Mapper.Map<Section>(SectionMv);
                    Section.StoreId = (int)user.StoreId;
                    _unitOfWork.SectionRepository.AddSection(Section);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The section added successfully", AlertType.Success);
                    return RedirectToAction("SectionList", "Products");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SectionList", "Products");


        }
        [HttpGet]
        public ActionResult UpdateSection(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SectionList");
            }
            ViewBag.edit = "UpdateSection";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SectionViewModel SectionMv =
                Mapper.Map<SectionViewModel>(_unitOfWork.SectionRepository.GetSectionById(id, (int)user.StoreId));
            return View("AddSection", SectionMv);
        }
        [HttpPost]
        public ActionResult UpdateSection(int id, SectionViewModel SectionMv)
        {
            ViewBag.edit = "UpdateSate";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Section Section = Mapper.Map<Section>(SectionMv);
                    _unitOfWork.SectionRepository.UpdateSection(id, Section, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The section updated successfully", AlertType.Success);
                    return RedirectToAction("SectionList", "Products");

                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SectionList", "Products");


        }
        public ActionResult DeleteSection(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.SectionRepository.DeleteSection(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The section deleted successfully", AlertType.Success);
                return RedirectToAction("SectionList", "Products");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }
            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SectionList", "Products");


        }


        //Recipe
        public JsonResult RecipeList(string productCode)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return Json(Mapper.Map<RecipeListViewModel[]>(_unitOfWork.RecipeRepository.GetRecipes((int)user.StoreId, productCode)), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddRecipe(string code)
        {
            RecipeViewModel RecipeVm = new RecipeViewModel();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            RecipeVm.ProductDDl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a=>a.InventoryItem && a.PurchaseItem)
                .Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name }).AsEnumerable();
            RecipeVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            RecipeVm.ProductCode = code;
            RecipeVm.StoreId = (int) user.StoreId;
            RecipeVm.RecipeList = _unitOfWork.RecipeRepository.GetRecipes(RecipeVm.StoreId, code).ToList();
            ViewBag.edit = "AddRecipe";
            return View(RecipeVm);
           
        }
        [HttpPost]
        public ActionResult AddRecipe(RecipeViewModel RecipeVm)
        {
            ViewBag.edit = "AddRecipe";
           
            try
            {
                    
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    RecipeVm.ProductDDl = _unitOfWork.ProductRepository.GetAllProducts(RecipeVm.StoreId).Where(a => a.InventoryItem && a.PurchaseItem)
                        .Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name }).AsEnumerable();
                    RecipeVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit(RecipeVm.StoreId)
                        .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                    RecipeVm.RecipeList = _unitOfWork.RecipeRepository.GetRecipes(RecipeVm.StoreId, RecipeVm.ProductCode).ToList();
                    return View(RecipeVm);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                

                    
                        Recipe Recipe = Mapper.Map<Recipe>(RecipeVm);
                        
                        _unitOfWork.RecipeRepository.AddRecipes(Recipe);

                    
                    _unitOfWork.Complete();

                    var product = _unitOfWork.ProductRepository.GetProductByCode(Recipe.ProductCode, Recipe.StoreId);
                    product.CostPrice = Convert.ToDouble(product.Recipes.Select(a =>
                            Convert.ToDecimal(a.Ingredient.CostPrice) *
                            a.Quantity)
                        .Sum());
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Recipe added successfully", AlertType.Success);
                    RecipeVm=new RecipeViewModel();
                    RecipeVm.ProductCode = Recipe.ProductCode;
                    RecipeVm.StoreId = Recipe.StoreId;
                    RecipeVm.ProductDDl = _unitOfWork.ProductRepository.GetAllProducts(RecipeVm.StoreId).Where(a => a.InventoryItem && a.PurchaseItem)
                        .Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name }).AsEnumerable();
                    RecipeVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit(RecipeVm.StoreId)
                        .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                    RecipeVm.RecipeList = _unitOfWork.RecipeRepository.GetRecipes(RecipeVm.StoreId, RecipeVm.ProductCode).ToList();
                    return View(RecipeVm);

                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }
            RecipeVm.ProductDDl = _unitOfWork.ProductRepository.GetAllProducts(RecipeVm.StoreId).Where(a => a.InventoryItem && a.PurchaseItem)
                .Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name }).AsEnumerable();
            RecipeVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit(RecipeVm.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            RecipeVm.RecipeList = _unitOfWork.RecipeRepository.GetRecipes(RecipeVm.StoreId, RecipeVm.ProductCode).ToList();
            return View(RecipeVm);


        }

        public ActionResult DeleteRecipe(int id,string code)
        {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
            try
            {
                _unitOfWork.RecipeRepository.DeleteRecipes(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Recipe deleted successfully", AlertType.Success);
                return View(_unitOfWork.RecipeRepository.GetRecipes(Convert.ToInt32(user.StoreId), code).ToList());
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] =
                            new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage,
                                AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                {
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message,
                                    AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                }
                else
                {

                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }

            }

            return View(_unitOfWork.RecipeRepository.GetRecipes(Convert.ToInt32(user.StoreId), code).ToList());



        }
        public ActionResult ProductDetail(string productId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var data = _unitOfWork.ProductRepository.GetProductByCode(productId, (int)user.StoreId);
            return View(data);
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