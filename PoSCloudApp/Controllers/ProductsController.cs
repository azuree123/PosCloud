using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;
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
        public ActionResult AddProduct(Product product)
        {
            ViewBag.edit = "AddProduct";
            if (ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                _unitOfWork.ProductRepository.AddProduct(product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }
            
        }
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.edit = "UpdateProduct";
            Product product = _unitOfWork.ProductRepository.GetProductById(id);
            return View("AddProduct", product);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id,Product product)
        {
            ViewBag.edit = "UpdateProduct";
            if (ModelState.IsValid)
            {
                return View("AddProduct", product);
            }
            else
            {
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
    }
}