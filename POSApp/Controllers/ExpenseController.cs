using System;
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

namespace POSApp.Controllers
{
    public class ExpenseController : Controller
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
        public ActionResult ExpenseList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ExpenseRepository.GetExpenses((int)user.StoreId));
        }
        public ActionResult AddExpense()
        {
            ExpenseViewModel expense=new ExpenseViewModel();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem{Value = a.Id.ToString(),Text = a.Name}).AsEnumerable();
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem {Text = a.Name,Value = a.Id.ToString()})
                .AsEnumerable();
            ViewBag.edit = "AddExpense";
           
            return View(expense);
        }
        [HttpPost]
        public ActionResult AddExpense(ExpenseViewModel expenseVm)
        {
            ViewBag.edit = "AddDevice";
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
                {
                    expenseVm.StoreId = user.StoreId;
                    Expense expense = Mapper.Map<Expense>(expenseVm);
                    _unitOfWork.ExpenseRepository.AddExpense(expense);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The expense added successfully", AlertType.Success);
                    return RedirectToAction("ExpenseList");
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
            }

            return RedirectToAction("ExpenseList", "Expense");


        }
        public ActionResult UpdateExpense(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ExpenseViewModel expense = Mapper.Map<ExpenseViewModel>(_unitOfWork.ExpenseRepository.GetExpenseById(id, Convert.ToInt32(user.StoreId)));
            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            ViewBag.edit = "UpdateExpense";
            return View("AddExpense",expense);
        }
        [HttpPost]
        public ActionResult UpdateExpense(int id,ExpenseViewModel expenseVm)
        {
            ViewBag.edit = "UpdateExpense";
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
                {
                    Expense expense = Mapper.Map<Expense>(expenseVm);
                    _unitOfWork.ExpenseRepository.UpdateExpense(id, expense, Convert.ToInt32(user.StoreId));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The expense updated successfully", AlertType.Success);
                    return RedirectToAction("ExpenseList");
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
            }

            return RedirectToAction("ExpenseList", "Expense");
           
            

        }
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
            }

            return RedirectToAction("ExpenseList", "Expense");

        }

        public ActionResult ExpenseHeadList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads((int)user.StoreId));
        }




        public ActionResult AddExpenseHeadPartial()
        {
            ViewBag.edit = "AddExpenseHeadPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddExpenseHeadPartial(ExpenseHeadViewModel expenseheadvm)
        {
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

                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
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
            }

            return PartialView("Test");


        }

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










        public ActionResult AddExpenseHead()
        {
            ViewBag.edit = "AddExpenseHead";
            return View();
        }
        [HttpPost]
        public ActionResult AddExpenseHead(ExpenseHeadViewModel expenseHeadVm)
        {
            ViewBag.edit = "AddExpenseHead";
            if (!ModelState.IsValid)
            {
                return View(expenseHeadVm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                expenseHeadVm.StoreId = user.StoreId;
                ExpenseHead expenseHead = Mapper.Map<ExpenseHead>(expenseHeadVm);
                _unitOfWork.ExpenseHeadRepository.AddExpenseHead(expenseHead);
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseHeadList");
            }
        }
        public ActionResult UpdateExpenseHead(int id)
        {
            ViewBag.edit = "UpdateExpenseHead";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ExpenseHeadViewModel expenseHeadVm =
                Mapper.Map<ExpenseHeadViewModel>(_unitOfWork.ExpenseHeadRepository.GetExpenseHeadById(id, Convert.ToInt32(user.StoreId)));
            return View("AddExpenseHead", expenseHeadVm);
        }
        [HttpPost]
        public ActionResult UpdateExpenseHead(int id,ExpenseHeadViewModel expenseHeadVm)
        {
            ViewBag.edit = "UpdateExpenseHead";
            if (!ModelState.IsValid)
            {
                return View("AddExpenseHead", expenseHeadVm);
            }
            else
            {
                ExpenseHead expenseHead = Mapper.Map<ExpenseHead>(expenseHeadVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ExpenseHeadRepository.UpdateExpenseHead(id,Convert.ToInt32(user.StoreId),expenseHead);
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseHeadList");
            }
        }
        public ActionResult DeleteExpenseHead(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ExpenseHeadRepository.DeleteExpenseHead(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ExpenseHeadList","Expense");
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