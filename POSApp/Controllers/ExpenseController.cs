using System;
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
using POSApp.Core.ViewModels;
using POSApp.SecurityFilters;

namespace POSApp.Controllers
{
    [Authorize]
    public class ExpenseController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public ExpenseController()
        {

        }

        public ExpenseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Expense
        [View(Config.Expense.Expense)]

        public ActionResult ExpenseList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            
            return View( _unitOfWork.ExpenseRepository.GetExpenses((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [View(Config.Expense.Expense)]

        public ActionResult DailyExpenseList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ExpenseRepository.GetExpensesByDate((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [View(Config.Expense.Expense)]

        public ActionResult AddExpense()
        {
            ExpenseViewModel expense=new ExpenseViewModel();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem {Text = a.Name,Value = a.Id.ToString()})
                .AsEnumerable();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ExpenseList");
            }
            ViewBag.edit = "AddExpense";
           
            return View(expense);
        }
        [HttpPost]
        [Manage(Config.Expense.Expense)]

        public ActionResult AddExpense(ExpenseViewModel expenseVm)
        {
            ViewBag.edit = "AddExpense";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            expenseVm.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();

            expenseVm.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            
            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(expenseVm);
                }
                else
                {
                    
                    Expense expense = Mapper.Map<Expense>(expenseVm);
                    _unitOfWork.ExpenseRepository.AddExpense(expense);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The expense added successfully", AlertType.Success);
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

            return View(expenseVm);


        }
        [Manage(Config.Expense.Expense)]

        public ActionResult UpdateExpense(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            ExpenseViewModel expense = Mapper.Map<ExpenseViewModel>(_unitOfWork.ExpenseRepository.GetExpenseById(id, Convert.ToInt32(user.StoreId)));
            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ExpenseList");
            }
            ViewBag.edit = "UpdateExpense";
            return View("AddExpense",expense);
        }
        [HttpPost]
        [Manage(Config.Expense.Expense)]

        public ActionResult UpdateExpense(int id,ExpenseViewModel expenseVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateExpense";
            expenseVm.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            expenseVm.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
           
            try
            {
               
                if (!ModelState.IsValid)
                {

                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddExpense", expenseVm);
                }
                else
                {
                    Expense expense = Mapper.Map<Expense>(expenseVm);
                    _unitOfWork.ExpenseRepository.UpdateExpense(id, expense, Convert.ToInt32(user.StoreId));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The expense updated successfully", AlertType.Success);
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

            return View("AddExpense",expenseVm);
           
            

        }

        [Manage(Config.Expense.Expense)]

        public ActionResult DeleteExpense(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ExpenseRepository.DeleteExpense(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The expense deleted successfully", AlertType.Success);
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


        [View(Config.Expense.Expense)]

        public ActionResult ExpenseHeadList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).OrderByDescending(a => a.Id));
        }



        [Manage(Config.Expense.Expense)]

        public ActionResult AddExpenseHeadPartial()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ExpenseHeadList");
            }
            ViewBag.edit = "AddExpenseHeadPartial";
            ExpenseHeadViewModel expenseHead = new ExpenseHeadViewModel();
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseHead.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            return View();
        }
        [HttpPost]
        [Manage(Config.Expense.Expense)]

        public ActionResult AddExpenseHeadPartial(ExpenseHeadViewModel expenseheadvm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseheadvm.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            ViewBag.edit = "AddExpenseHeadPartial";
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

                   
                    expenseheadvm.StoreId = user.StoreId;
                    ExpenseHead expensehead = Mapper.Map<ExpenseHead>(expenseheadvm);
                   
                    _unitOfWork.ExpenseHeadRepository.AddExpenseHead(expensehead);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Expense added successfully", AlertType.Success);
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
        [Manage(Config.Expense.Expense)]

        public JsonResult GetExpenseHeadDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ExpenseHeadViewModel[]>(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [Manage(Config.Expense.Expense)]

        public ActionResult AddExpenseHead()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ExpenseHeadList");
            }
            ViewBag.edit = "AddExpenseHead";
            ExpenseHeadViewModel expenseHead = new ExpenseHeadViewModel();
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseHead.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            return View();
        }
        [HttpPost]
        [Manage(Config.Expense.Expense)]

        public ActionResult AddExpenseHead(ExpenseHeadViewModel expenseHeadVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddExpenseHead";
            
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseHeadVm.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(expenseHeadVm);
                }
                else
                {
                  
                    expenseHeadVm.StoreId = user.StoreId;
                    ExpenseHead expenseHead = Mapper.Map<ExpenseHead>(expenseHeadVm);
                    _unitOfWork.ExpenseHeadRepository.AddExpenseHead(expenseHead);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Expense head added successfully", AlertType.Success);
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

            return View(expenseHeadVm);

        }

        [Manage(Config.Expense.Expense)]

        public ActionResult UpdateExpenseHead(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ExpenseHeadList");
            }
            ViewBag.edit = "UpdateExpenseHead";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ExpenseHeadViewModel expenseHeadVm =
                Mapper.Map<ExpenseHeadViewModel>(_unitOfWork.ExpenseHeadRepository.GetExpenseHeadById(id, Convert.ToInt32(user.StoreId)));
           
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseHeadVm.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            return View("AddExpenseHead", expenseHeadVm);
        }
        [HttpPost]
        [Manage(Config.Expense.Expense)]

        public ActionResult UpdateExpenseHead(int id,ExpenseHeadViewModel expenseHeadVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateExpenseHead";
            
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            expenseHeadVm.StoreDdl = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddExpenseHead",expenseHeadVm);
                }
                else
                {
                    ExpenseHead expenseHead = Mapper.Map<ExpenseHead>(expenseHeadVm);
                    
                    _unitOfWork.ExpenseHeadRepository.UpdateExpenseHead(id, Convert.ToInt32(user.StoreId), expenseHead);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Expense head updated successfully", AlertType.Success);
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

            return View("AddExpenseHead",expenseHeadVm);

        }

        [Manage(Config.Expense.Expense)]

        public ActionResult DeleteExpenseHead(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ExpenseHeadRepository.DeleteExpenseHead(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Expense head deleted successfully", AlertType.Success);
                return RedirectToAction("ExpenseHeadList", "Expense");
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

            return RedirectToAction("ExpenseHeadList");

        }

        public ActionResult GetEmployeeInfo(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            //try
            //{
            //    var userid = User.Identity.GetUserId();
            //    var user = UserManager.FindById(userid);
            //    var storeEmployees = _unitOfWork.EmployeeRepository.GetStoreEmployee(user.StoreId);

            //    EmployeeDdl employee =
            //        Mapper.Map<EmployeeDdl>(
            //            storeEmployees.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable());
            //    return Json(employee, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
            List<Employee> employee = new List<Employee>();
           
            employee = _unitOfWork.EmployeeRepository.GetEmployees(user.StoreId).Where(a => a.StoreId == id).ToList();
            SelectList employeeSelect = new SelectList(employee, "Id", "Name", 0);
            return Json(employeeSelect);
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