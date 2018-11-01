﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Services;

namespace POSApp.Controllers
{
    public class ExcelImportController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;


        public ExcelImportController()
        {

        }

        public ExcelImportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ExcelImport
        public ActionResult StateExcelImport()
        {
            ViewBag.edit = "StateExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult StateExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
                {
                    State NewModel = new State();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                    NewModel.Name = dr["Name"].ToString();
                    _unitOfWork.StateRepository.AddState(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
                return RedirectToAction("StateList", "Setup");
        }
        [HttpGet]
        public ActionResult CityExcelImport()
        {
            ViewBag.edit = "CityExcelImport";
            return View();
        }
        [HttpPost]
        public ActionResult CityExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
                {
                    City NewModel = new City();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.StateId = Int32.Parse(dr["StateId"].ToString());
                        _unitOfWork.CityRepository.AddCity(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("CityList", "Setup");
        }

        public ActionResult TaxExcelImport()
        {
            ViewBag.edit = "TaxExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult TaxExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Tax NewModel = new Tax();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Rate = double.Parse(dr["Rate"].ToString());
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.TaxRepository.AddTax(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("TaxList", "Setup");
        }

        public ActionResult DiscountExcelImport()
        {
            ViewBag.edit = "DiscountExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult DiscountExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Discount NewModel = new Discount();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Amount = double.Parse(dr["Amount"].ToString()) ;
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.DiscountRepository.AddDiscount(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("DiscountList", "Setup");
        }
        public ActionResult CouponExcelImport()
        {
            ViewBag.edit = "CouponExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult CouponExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Coupon NewModel = new Coupon();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Amount = int.Parse(dr["Amount"].ToString());
                        NewModel.Value = double.Parse(dr["Value"].ToString());
                        NewModel.Days = dr["Days"].ToString();
                        NewModel.ValidFrom = DateTime.Parse(dr["ValidFrom"].ToString());
                        NewModel.ValidFrom = DateTime.Parse(dr["ValidTill"].ToString());
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.CouponRepository.AddCoupon(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("CouponList", "Setup");
        }

        public ActionResult CustomerExcelImport()
        {
            ViewBag.edit = "CustomerExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult CustomerExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    BusinessPartner NewModel = new BusinessPartner();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.PhoneNumber = dr["PhoneNumber"].ToString();
                        NewModel.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                        NewModel.Address = dr["Address"].ToString();
                        NewModel.StoreId = (int)user.StoreId;
                        NewModel.Type = "C";
                        _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("CustomerList", "Setup");
        }

        public ActionResult DepartmentExcelImport()
        {
            ViewBag.edit = "DepartmentExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult DepartmentExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
                {
                    Department NewModel = new Department();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.StoreId = (int) user.StoreId;
                        _unitOfWork.DepartmentRepository.AddDepartment(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("DepartmentList", "Setup");
        }

        //Employee
        public ActionResult EmployeeExcelImport()
        {
            ViewBag.edit = "EmployeeExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Employee NewModel = new Employee();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Email = dr["Email"].ToString();
                        NewModel.MobileNumber = dr["MobileNumber"].ToString();
                        NewModel.JoinDate = DateTime.Parse(dr["JoinDate"].ToString());
                        NewModel.Salary = double.Parse(dr["Salary"].ToString());
                        NewModel.Commission = double.Parse(dr["Commission"].ToString());
                        NewModel.DepartmentId = Int32.Parse(dr["DepartmentId"].ToString());
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.EmployeeRepository.AddEmployee(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("EmployeeList", "Setup");
        }

        //Expense

        public ActionResult ExpenseExcelImport()
        {
            ViewBag.edit = "ExpenseExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult ExpenseExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Expense NewModel = new Expense();
                    if (!string.IsNullOrWhiteSpace(dr["Amount"].ToString()))
                    {
                        NewModel.Date = DateTime.Parse(dr["Date"].ToString());
                        NewModel.Amount = double.Parse(dr["Amount"].ToString());
                        NewModel.ExpenseHeadId = Int32.Parse(dr["ExpenseHeadId"].ToString());
                        NewModel.EmployeeId = Int32.Parse(dr["EmployeeId"].ToString());
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.ExpenseRepository.AddExpense(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("ExpenseList", "Expense");
        }

        //ExpenseHead

        public ActionResult ExpenseHeadExcelImport()
        {
            ViewBag.edit = "ExpenseHeadExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult ExpenseHeadExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    ExpenseHead NewModel = new ExpenseHead();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Details = dr["Details"].ToString();
                        NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.ExpenseHeadRepository.AddExpenseHead(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("ExpenseHeadList", "Expense");
        }

        //Store

        public ActionResult StoreExcelImport()
        {
            ViewBag.edit = "StoreExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult StoreExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
                {
                    Store NewModel = new Store();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Address = dr["Address"].ToString();
                        //NewModel.StoreId = (int)user.StoreId;
                        _unitOfWork.StoreRepository.AddStore(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("StoresList", "Store");
        }

        //Supplier

        public ActionResult SupplierExcelImport()
        {
            ViewBag.edit = "SupplierExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult SupplierExcelImport(HttpPostedFileBase file)
        {
            
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    BusinessPartner NewModel = new BusinessPartner();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.ContactPerson = dr["ContactPerson"].ToString();
                        NewModel.PhoneNumber = dr["PhoneNumber"].ToString();
                        NewModel.CpMobileNumber = dr["CpMobileNumber"].ToString();
                        NewModel.StoreId = (int)user.StoreId;
                        NewModel.Type = "S";
                        NewModel.Birthday=DateTime.Now;
                        _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("SupplierList", "Setup");
        }

        //Product

        public ActionResult ProductExcelImport()
        {
            ViewBag.edit = "ProductExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult ProductExcelImport(HttpPostedFileBase file)
        {
                
             DataTable dt= ImportService.GetExcelData(file);

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Product NewModel = new Product();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Description = dr["Description"].ToString();
                        NewModel.ProductCode = dr["ProductCode"].ToString();
                        NewModel.Attribute = dr["Attribute"].ToString();
                        NewModel.Size = dr["Size"].ToString();
                        if (!string.IsNullOrEmpty(dr["TaxId"].ToString()))
                        {
                        NewModel.TaxId =Int32.Parse(dr["TaxId"].ToString());
                        }
                        NewModel.UnitPrice =double.Parse(dr["UnitPrice"].ToString());
                        NewModel.CostPrice = double.Parse(dr["CostPrice"].ToString());
                        NewModel.Stock = double.Parse(dr["Stock"].ToString());
                        NewModel.ReOrderLevel = Int32.Parse(dr["ReOrderLevel"].ToString());
                        NewModel.Barcode = dr["Barcode"].ToString();
                        NewModel.CategoryId = Int32.Parse(dr["CategoryId"].ToString());
                        NewModel.UnitId = Int32.Parse(dr["UnitId"].ToString());
                        NewModel.StoreId = (int)user.StoreId;
                        NewModel.Type = "Product";
                        _unitOfWork.ProductRepository.AddProduct(NewModel);
                    }
                }
                _unitOfWork.Complete();

            
            return RedirectToAction("ProductsList", "Products");
        }

        //ProductCategory

        public ActionResult ProductCategoryExcelImport()
        {
            ViewBag.edit = "ProductCategoryExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult ProductCategoryExcelImport(HttpPostedFileBase file)
        {

            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
            {
                ProductCategory NewModel = new ProductCategory();
                if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                {
                    NewModel.Name = dr["Name"].ToString();
                    NewModel.Type = dr["Type"].ToString();
                    NewModel.StoreId = (int) user.StoreId;
                    _unitOfWork.ProductCategoryRepository.AddProductCategory(NewModel);
                }
            }
            _unitOfWork.Complete();


            return RedirectToAction("ProductCategoryList", "Products");
        }

        //ProductCategory Group

        public ActionResult ProductCategoryGroupExcelImport()
        {
            ViewBag.edit = "ProductCategoryGroupExcelImport";
            return View();
        }

        [HttpPost]
        public ActionResult ProductCategoryGroupExcelImport(HttpPostedFileBase file)
        {

            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            foreach (DataRow dr in dt.Rows)
            {
                ProductCategoryGroup NewModel = new ProductCategoryGroup();
                if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                {
                    NewModel.Name = dr["Name"].ToString();
                    NewModel.StoreId = (int)user.StoreId;
                    _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(NewModel);
                }
            }
            _unitOfWork.Complete();


            return RedirectToAction("ProductCategoryGroupList", "Products");
        }

      

     
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
    }
}