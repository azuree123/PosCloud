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
        public ActionResult ExpensesList()
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
        public ActionResult DeleteExpense()
        {
            return View();
        }

        public ActionResult ExpenseHeadList()
        {
            return View(_unitOfWork.ExpenseHeadRepository.GetExpenseHeads());
        }
        public ActionResult AddExpenseHead()
        {
            return View();
        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult DeleteExpenseHead()
        {
            return View();
        }
    }
}