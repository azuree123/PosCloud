using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PoSCloudApp.Core;
using PoSCloudApp.Core.Dtos;
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
            return View(_unitOfWork.ProductRepository.GetAllProducts());
        }

        public ActionResult AddProduct()
        {
            ViewBag.edit = "AddProduct";
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductCreateUpdateDto productDto)
        {
            ViewBag.edit = "AddProduct";
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }
            else
            {
                Product product = Mapper.Map<Product>(productDto);
                _unitOfWork.ProductRepository.AddProduct(product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }
            
        }
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.edit = "UpdateProduct";
            ProductCreateUpdateDto productDto = Mapper.Map<ProductCreateUpdateDto>(_unitOfWork.ProductRepository.GetProductById(id));
            return View("AddProduct", productDto);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id,ProductCreateUpdateDto productDto)
        {
            ViewBag.edit = "UpdateProduct";
            if (!ModelState.IsValid)
            {
                return View("AddProduct", productDto);
            }
            else
            {
                Product product = Mapper.Map<Product>(productDto);
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
        public ActionResult AddProductCategory(ProductCategoryDto productCategory)
        {
            ViewBag.edit = "AddProductCategory";
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                ProductCategory category = Mapper.Map<ProductCategory>(productCategory);
                _unitOfWork.ProductCategoryRepository.AddProductCategory(category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }
        public ActionResult UpdateProductCategory(int id)
        {
            ViewBag.edit = "UpdateProductCategory";
            ProductCategoryDto product =
                Mapper.Map<ProductCategoryDto>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
            return View("AddProductCategory", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory(int id,ProductCategoryDto productCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateProductCategory";
                ProductCategoryDto product =
                    Mapper.Map<ProductCategoryDto>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id));
                return View("AddProductCategory", product);
            }
            else
            {
                ProductCategory category = Mapper.Map<ProductCategory>(productCategoryDto);
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