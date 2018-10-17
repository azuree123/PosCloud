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
            ExpenseViewModel expense = Mapper.Map<ExpenseViewModel>(_unitOfWork.ExpenseRepository.GetExpenseById(id));
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
                _unitOfWork.ExpenseRepository.UpdateExpense(id,expense);
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseList");
            }

        }
        public ActionResult DeleteExpense(int id)
        {
            _unitOfWork.ExpenseRepository.DeleteExpense(id);
            _unitOfWork.Complete();
            return RedirectToAction("ExpenseList", "Expense");
        }

        public ActionResult ExpenseHeadList()
        {
            return View(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads());
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
            ExpenseHeadViewModel expenseHeadVm =
                Mapper.Map<ExpenseHeadViewModel>(_unitOfWork.ExpenseHeadRepository.GetExpenseHeadById(id));
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
                _unitOfWork.ExpenseHeadRepository.UpdateExpenseHead(id,expenseHead);
                _unitOfWork.Complete();
                return RedirectToAction("ExpenseHeadList");
            }
        }
        public ActionResult DeleteExpenseHead(int id)
        {
            _unitOfWork.ExpenseHeadRepository.DeleteExpenseHead(id);
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