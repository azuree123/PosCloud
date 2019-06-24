using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.SecurityFilters;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class ExcelImportController : LanguageController
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
        [View(Config.ExcelImport.DesignationExcelImport)]

        public ActionResult DesignationExcelImport()
        {
            ViewBag.edit = "DesignationExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.DesignationExcelImport)]

        public ActionResult DesignationExcelImport(HttpPostedFileBase file)
        {

            try
            {
                DataTable dt = ImportService.GetExcelData(file);

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Designation NewModel = new Designation();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.DesignationRepository.AddDesignation(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("DesignationList", "Setup");
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



            return RedirectToAction("DesignationList", "Setup");
        }
        // GET: ExcelImport
        public ActionResult StateExcelImport()
        {
            ViewBag.edit = "StateExcelImport";
            return View();
        }

        [HttpPost]


        [Manage(Config.ExcelImport.StateExcelImport)]


        public ActionResult StateExcelImport(HttpPostedFileBase file)
        {
            

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            try
            {
            DataTable dt = ImportService.GetExcelData(file);
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
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("StateList", "Setup");
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

            return RedirectToAction("StateList", "Setup");
        }

        [HttpGet]
        [Manage(Config.ExcelImport.CityExcelImport)]

        public ActionResult CityExcelImport()
        {
            ViewBag.edit = "CityExcelImport";
            return View();
        }
        [HttpPost]
        [Manage(Config.ExcelImport.CityExcelImport)]

        public ActionResult CityExcelImport(HttpPostedFileBase file)
        {
            

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            try
            {
            DataTable dt = ImportService.GetExcelData(file);
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
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("CityList", "Setup");
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



            return RedirectToAction("CityList", "Setup");
        }
        [Manage(Config.ExcelImport.TaxExcelImport)]

        public ActionResult TaxExcelImport()
        {
            ViewBag.edit = "TaxExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.TaxExcelImport)]

        public ActionResult TaxExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.TaxRepository.AddTax(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("TaxList", "Setup");
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



            return RedirectToAction("TaxList", "Setup");
        }
        [Manage(Config.ExcelImport.DiscountExcelImport)]

        public ActionResult DiscountExcelImport()
        {
            ViewBag.edit = "DiscountExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.DiscountExcelImport)]

        public ActionResult DiscountExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.IsPercentage = Convert.ToBoolean(dr["IsPercentage"].ToString());
                        NewModel.IsTaxable = Convert.ToBoolean(dr["IsTaxable"].ToString());
                        NewModel.Type = dr["Type"].ToString();
                        NewModel.DiscountCode = dr["DiscountCode"].ToString();
                        NewModel.Value = decimal.Parse(dr["Value"].ToString());
                        NewModel.Days = dr["Days"].ToString();
                        NewModel.ValidFrom = DateTime.Parse(dr["ValidFrom"].ToString());
                        NewModel.ValidTill = DateTime.Parse(dr["ValidTill"].ToString());
                        NewModel.IsActive = bool.Parse(dr["IsActive"].ToString());
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.DiscountRepository.AddDiscount(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("DiscountList", "Setup");
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
            return RedirectToAction("DiscountList", "Setup");
        }

        [Manage(Config.ExcelImport.CouponExcelImport)]

        public ActionResult CouponExcelImport()
        {
            ViewBag.edit = "CouponExcelImport";
            return View();
        }

        //public ActionResult CouponExcelImport(HttpPostedFileBase file)
        //{

        //    DataTable dt = ImportService.GetExcelData(file);

        //    var userid = User.Identity.GetUserId();
        //        var user = UserManager.FindById(userid);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            Discount NewModel = new Discount();
        //            if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
        //            {
        //                NewModel.Name = dr["Name"].ToString();
        //                //NewModel.Amount = int.Parse(dr["Amount"].ToString());
        //                NewModel.Value = decimal.Parse(dr["Value"].ToString());

        //                NewModel.Days = dr["Days"].ToString();
        //                NewModel.ValidFrom = DateTime.Parse(dr["ValidFrom"].ToString());
        //                NewModel.ValidFrom = DateTime.Parse(dr["ValidTill"].ToString());
        //                NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
        //                _unitOfWork.DiscountRepository.AddDiscount(NewModel);
        //            }
        //        }
        //        _unitOfWork.Complete();


        //    return RedirectToAction("CouponList", "Setup");
        //}
        [Manage(Config.ExcelImport.CustomerExcelImport)]

        public ActionResult CustomerExcelImport()
        {
            ViewBag.edit = "CustomerExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.CustomerExcelImport)]

        public ActionResult CustomerExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        NewModel.Type = "C";
                        _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("CustomerList", "Setup");
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
            return RedirectToAction("CustomerList", "Setup");
        }
        [Manage(Config.ExcelImport.DepartmentExcelImport)]

        public ActionResult DepartmentExcelImport()
        {
            ViewBag.edit = "DepartmentExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.DepartmentExcelImport)]

        public ActionResult DepartmentExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.DepartmentRepository.AddDepartment(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("DepartmentList", "Setup");
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
            return RedirectToAction("DepartmentList", "Setup");
        }

        //Employee
        [Manage(Config.ExcelImport.EmployeeExcelImport)]

        public ActionResult EmployeeExcelImport()
        {
            ViewBag.edit = "EmployeeExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.EmployeeExcelImport)]

        public ActionResult EmployeeExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.EmployeeRepository.AddEmployee(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("EmployeeList", "Setup");
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
            return RedirectToAction("EmployeeList", "Setup");
        }

        //Expense
        [Manage(Config.ExcelImport.ExpenseExcelImport)]

        public ActionResult ExpenseExcelImport()
        {
            ViewBag.edit = "ExpenseExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ExpenseExcelImport)]

        public ActionResult ExpenseExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.ExpenseRepository.AddExpense(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("ExpenseList", "Expense");
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
            return RedirectToAction("ExpenseList", "Expense");
        }

        //ExpenseHead
        [Manage(Config.ExcelImport.ExpenseHeadExcelImport)]

        public ActionResult ExpenseHeadExcelImport()
        {
            ViewBag.edit = "ExpenseHeadExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ExpenseHeadExcelImport)]

        public ActionResult ExpenseHeadExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.ExpenseHeadRepository.AddExpenseHead(NewModel);
                        _unitOfWork.Complete();
                        TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                        return RedirectToAction("ExpenseHeadList", "Expense");
                    }
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

            return RedirectToAction("ExpenseHeadList", "Expense");
        }

        //Store
        [Manage(Config.ExcelImport.StoreExcelImport)]

        public ActionResult StoreExcelImport()
        {
            ViewBag.edit = "StoreExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.StoreExcelImport)]

        public ActionResult StoreExcelImport(HttpPostedFileBase file)
        {
            

            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            try
            {
            DataTable dt = ImportService.GetExcelData(file);
                foreach (DataRow dr in dt.Rows)
                {
                    Store NewModel = new Store();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Address = dr["Address"].ToString();
                        //NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.StoreRepository.AddStore(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("StoresList", "Store");
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
            return RedirectToAction("StoresList", "Store");
        }

        //Supplier
        [Manage(Config.ExcelImport.SupplierExcelImport)]

        public ActionResult SupplierExcelImport()
        {
            ViewBag.edit = "SupplierExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.SupplierExcelImport)]

        public ActionResult SupplierExcelImport(HttpPostedFileBase file)
        {
            
            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        NewModel.Type = "S";
                        NewModel.Birthday = DateTime.Now;
                        _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("SupplierList", "Setup");
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
            return RedirectToAction("SupplierList", "Setup");
        }

        //Product
        [Manage(Config.ExcelImport.ProductExcelImport)]

        public ActionResult ProductExcelImport()
        {
            ViewBag.edit = "ProductExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ProductExcelImport)]

        public ActionResult ProductExcelImport(HttpPostedFileBase file)
        {
                
            try
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
                            NewModel.TaxId = Int32.Parse(dr["TaxId"].ToString());
                        }
                        NewModel.UnitPrice = double.Parse(dr["UnitPrice"].ToString());
                        NewModel.CostPrice = double.Parse(dr["CostPrice"].ToString());
                        NewModel.Stock = double.Parse(dr["Stock"].ToString());
                        NewModel.ReOrderLevel = Int32.Parse(dr["ReOrderLevel"].ToString());
                        NewModel.Barcode = dr["Barcode"].ToString();
                        NewModel.CategoryId = Int32.Parse(dr["CategoryId"].ToString());
                        NewModel.UnitId = Int32.Parse(dr["UnitId"].ToString());
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        NewModel.Type = "Product";
                        _unitOfWork.ProductRepository.AddProduct(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
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
            return RedirectToAction("ProductsList", "Products");
        }

        //ProductCategory
        [Manage(Config.ExcelImport.ProductCategoryExcelImport)]

        public ActionResult ProductCategoryExcelImport()
        {
            ViewBag.edit = "ProductCategoryExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ProductCategoryExcelImport)]

        public ActionResult ProductCategoryExcelImport(HttpPostedFileBase file)
        {

            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.ProductCategoryRepository.AddProductCategory(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
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

        //ProductCategory Group
        [Manage(Config.ExcelImport.ProductCategoryGroupExcelImport)]

        public ActionResult ProductCategoryGroupExcelImport()
        {
            ViewBag.edit = "ProductCategoryGroupExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ProductCategoryGroupExcelImport)]

        public ActionResult ProductCategoryGroupExcelImport(HttpPostedFileBase file)
        {

            try
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
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
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

        [Manage(Config.ExcelImport.DeviceExcelImport)]

        public ActionResult DeviceExcelImport()
        {
            ViewBag.edit = "DeviceExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.DeviceExcelImport)]

        public ActionResult DeviceExcelImport(HttpPostedFileBase file)
        {

            try
            {
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Device NewModel = new Device();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.License = dr["License"].ToString();
                        NewModel.DeviceCode = dr["DeviceCode"].ToString();
                        NewModel.AppVersion = dr["AppVersion"].ToString();
                        NewModel.Address = dr["Address"].ToString();

                        NewModel.Contact = dr["Contact"].ToString();
                        NewModel.DownloadedDate = DateTime.Parse(dr["DownloadedDate"].ToString());

                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.DeviceRepository.AddDevice(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("DeviceList", "Device");
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
            return RedirectToAction("DeviceList", "Device");
        }

        [Manage(Config.ExcelImport.FloorExcelImport)]

        public ActionResult FloorExcelImport()
        {
            ViewBag.edit = "FloorExcelImport";
            return View();
        }
        [HttpPost]
        [Manage(Config.ExcelImport.FloorExcelImport)]

        public ActionResult FloorExcelImport(HttpPostedFileBase file)
        {

            try
            {
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Floor NewModel = new Floor();
                    if (!string.IsNullOrWhiteSpace(dr["FloorNumber"].ToString()))
                    {
                        NewModel.FloorNumber = dr["FloorNumber"].ToString();


                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.FloorRepository.AddFloor(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("FloorList", "Setup");
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
            return RedirectToAction("FloorList", "Setup");
        }

        [Manage(Config.ExcelImport.DineTableExcelImport)]

        public ActionResult DineTableExcelImport()
        {
            ViewBag.edit = "DineTableExcelImport";
            return View();
        }
        [HttpPost]
        [Manage(Config.ExcelImport.DineTableExcelImport)]

        public ActionResult DineTableExcelImport(HttpPostedFileBase file)
        {

            try
            {
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    DineTable NewModel = new DineTable();
                    if (!string.IsNullOrWhiteSpace(dr["DineTableNumber"].ToString()))
                    {
                        NewModel.DineTableNumber = dr["DineTableNumber"].ToString();
                        string floorNames = dr["FloorNumber"].ToString();
                        Floor checkFloor = _unitOfWork.FloorRepository.GetFloorByFloorNumber(floorNames, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                        if (checkFloor == null)
                        {
                            var data = new Floor { FloorNumber = floorNames, StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) };
                            _unitOfWork.FloorRepository.AddFloor(data);
                            _unitOfWork.Complete();
                            NewModel.FloorId = data.Id;
                        }
                        else
                        {
                            NewModel.FloorId = checkFloor.Id;
                        }

                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.DineTableRepository.AddDineTable(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("DineTableList", "Setup");
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
            return RedirectToAction("DineTableList", "Setup");
        }
        [Manage(Config.ExcelImport.ClientExcelImport)]

        public ActionResult ClientExcelImport()
        {
            ViewBag.edit = "ClientExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ClientExcelImport)]

        public ActionResult ClientExcelImport(HttpPostedFileBase file)
        {


            //var userid = User.Identity.GetUserId();
            //var user = UserManager.FindById(userid);
            try
            {
            DataTable dt = ImportService.GetExcelData(file);
                foreach (DataRow dr in dt.Rows)
                {
                    Client NewModel = new Client();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.Address = dr["Address"].ToString();
                        NewModel.Contact = dr["Contact"].ToString();
                        _unitOfWork.ClientRepository.AddClient(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("ClientList", "Setup");
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
            return RedirectToAction("ClientList", "Setup");
        }

        [Manage(Config.ExcelImport.UnitExcelImport)]

        public ActionResult UnitExcelImport()
        {
            ViewBag.edit = "UnitExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.UnitExcelImport)]

        public ActionResult UnitExcelImport(HttpPostedFileBase file)
        {

            try
            {
            DataTable dt = ImportService.GetExcelData(file);

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Unit NewModel = new Unit();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        NewModel.UnitCode = dr["UnitCode"].ToString();
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.UnitRepository.AddUnit(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("UnitList", "Setup");
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
            return RedirectToAction("UnitList", "Setup");
        }

        [Manage(Config.ExcelImport.SectionExcelImport)]

        public ActionResult SectionExcelImport()
        {
            ViewBag.edit = "SectionExcelImport";
            return View();
        }
        [HttpPost]
        [Manage(Config.ExcelImport.SectionExcelImport)]

        public ActionResult SectionExcelImport(HttpPostedFileBase file)
        {

            try
            {
                DataTable dt = ImportService.GetExcelData(file);

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Section NewModel = new Section();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.SectionRepository.AddSection(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
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
        [Manage(Config.ExcelImport.ShiftExcelImport)]

        public ActionResult ShiftExcelImport()
        {
            ViewBag.edit = "ShiftExcelImport";
            return View();
        }

        [HttpPost]
        [Manage(Config.ExcelImport.ShiftExcelImport)]

        public ActionResult ShiftExcelImport(HttpPostedFileBase file)
        {

            try
            {
                DataTable dt = ImportService.GetExcelData(file);

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Shift NewModel = new Shift();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();
                        
                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.ShiftRepository.AddShift(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("ShiftList", "Setup");
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
            return RedirectToAction("ShiftList", "Setup");
        }
        [Manage(Config.ExcelImport.SizeExcelImport)]

        public ActionResult SizeExcelImport()
        {
            ViewBag.edit = "SizeExcelImport";
            return View();
        }
        [HttpPost]
        [Manage(Config.ExcelImport.SizeExcelImport)]

        public ActionResult SizeExcelImport(HttpPostedFileBase file)
        {

            try
            {
                DataTable dt = ImportService.GetExcelData(file);

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                foreach (DataRow dr in dt.Rows)
                {
                    Size NewModel = new Size();
                    if (!string.IsNullOrWhiteSpace(dr["Name"].ToString()))
                    {
                        NewModel.Name = dr["Name"].ToString();


                        NewModel.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                        _unitOfWork.SizeRepository.AddSize(NewModel);
                    }
                }
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The data added successfully", AlertType.Success);
                return RedirectToAction("SizeList", "Setup");
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
            return RedirectToAction("SizeList", "Setup");
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
    }
}