using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;

namespace PoSCloudApp.Controllers
{
    public class ExpenseController : Controller
    {
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
            return View();
        }
        public ActionResult UpdateExpense()
        {
            return View();
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
            return View();
        }
        public ActionResult UpdateExpenseHead()
        {
            return View();
        }
        public ActionResult DeleteExpenseHead(int id)
        {
            _unitOfWork.ExpenseHeadRepository.DeleteExpenseHead(id);
            _unitOfWork.Complete();
            return RedirectToAction("ExpenseHeadList","Expense");
        }
    }
}