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
            return View(_unitOfWork.ExpenseRepository.GetExpenses());
        }
        public ActionResult AddExpense()
        {
            ExpenseViewModel expense=new ExpenseViewModel();
            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees().Select(a => new SelectListItem{Value = a.Id.ToString(),Text = a.Name}).AsEnumerable();
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads().Select(a => new SelectListItem {Text = a.Name,Value = a.Id.ToString()})
                .AsEnumerable();
            ViewBag.edit = "AddExpense";
           
            return View(expense);
        }
        [HttpPost]
        public ActionResult AddExpense(ExpenseViewModel expenseVm)
        {
            if (!ModelState.IsValid)
            {
                expenseVm.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                expenseVm.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                    .AsEnumerable();
                ViewBag.edit = "AddExpense";
                return View(expenseVm);

            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                expenseVm.StoreId = user.StoreId;
                Expense expense = Mapper.Map<Expense>(expenseVm);
                _unitOfWork.ExpenseRepository.AddExpense(expense);
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseList");
            }
           
        }
        public ActionResult UpdateExpense(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ExpenseViewModel expense = Mapper.Map<ExpenseViewModel>(_unitOfWork.ExpenseRepository.GetExpenseById(id, Convert.ToInt32(user.StoreId)));
            expense.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            expense.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .AsEnumerable();
            ViewBag.edit = "UpdateExpense";
            return View("AddExpense",expense);
        }
        [HttpPost]
        public ActionResult UpdateExpense(int id,ExpenseViewModel expenseVm)
        {
            if (!ModelState.IsValid)
            {

                expenseVm.EmpDdl = _unitOfWork.EmployeeRepository.GetEmployees().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                expenseVm.ExpHeadDdl = _unitOfWork.ExpenseHeadRepository.GetExpenseHeads().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                    .AsEnumerable();
                ViewBag.edit = "UpdateExpense";
                return View("AddExpense", expenseVm);

            }
            else
            {
                Expense expense = Mapper.Map<Expense>(expenseVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ExpenseRepository.UpdateExpense(id,expense, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseList");
            }

        }
        public ActionResult DeleteExpense(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ExpenseRepository.DeleteExpense(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ExpenseList", "Expense");
        }

        public ActionResult ExpenseHeadList()
        {
            return View(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads());
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
            if (!ModelState.IsValid)
            {
                return View(expenseheadvm);
            }
            else
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                expenseheadvm.StoreId = user.StoreId;
                ExpenseHead expensehead = Mapper.Map<ExpenseHead>(expenseheadvm);
                _unitOfWork.ExpenseHeadRepository.AddExpenseHead(expensehead);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }

        public JsonResult GetExpenseHeadDdl()
        {
            try
            {
                return Json(Mapper.Map<ExpenseHeadViewModel[]>(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads()), JsonRequestBehavior.AllowGet);
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