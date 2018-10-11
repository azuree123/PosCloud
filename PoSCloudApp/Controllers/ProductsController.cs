using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PoSCloudApp.Core;
using PoSCloudApp.Core.ViewModels;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Persistence;

namespace PoSCloudApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
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
                return View(productVm);
            }
            else 
            if (file !=null && file.ContentLength > 0)
            {
                
                try
                {
                    string path = Server.MapPath("~/Images/Data/Product" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productVm.Image = "/Images/Data/Product";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
                
            }
            {
                Product product = Mapper.Map<Product>(productVm);
                _unitOfWork.ProductRepository.AddProduct(product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }
            
        }
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.edit = "UpdateProduct";
            ProductCreateViewModel productVm = Mapper.Map<ProductCreateViewModel>(_unitOfWork.ProductRepository.GetProductById(id));
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
                    string path = Server.MapPath("~/Images/Data/Product" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productVm.Image = "/Images/Data/Product";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
            }

            {
                Product product = Mapper.Map<Product>(productVm);
                _unitOfWork.ProductRepository.UpdateProduct(id,product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }

        }
        public ActionResult DeleteProduct(int id)
        {
            _unitOfWork.ProductRepository.DeleteProduct(id);
            _unitOfWork.Complete();
            return RedirectToAction("ProductsList", "Products");
        }

        public ActionResult ProductCategoryList()
        {
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories());
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
                    string path = Server.MapPath("~/Images/Data/Product" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productCategory.Image = "/Images/Data/Product";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
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
            ProductCategoryViewModel product =
                Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
            return View("AddProductCategory", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory(int id, ProductCategoryViewModel productCategoryVm, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateProductCategory";
                ProductCategoryViewModel product =
                    Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
                return View("AddProductCategory", product);
            }
            else
            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    string path = Server.MapPath("~/Images/Data/Product" + file.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Message = "Image Already Exists!";
                    }
                    else
                    {
                        file.SaveAs(path);
                        productCategoryVm.Image = "/Images/Data/Product";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
                productCategoryVm.Type = "Product";
                ProductCategory category = Mapper.Map<ProductCategory>(productCategoryVm);
                _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id,category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }

        public ActionResult DeleteProductCategory(int id)
        {
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id);
            _unitOfWork.Complete();
            return RedirectToAction("ProductCategoryList", "Products");
        }
    }
}