using System;
using System.Linq;
using System.Web.Mvc;
using POSApp.Core;

namespace POSApp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Reports
        public ActionResult SaleReport()
        {
            return View();
        }

        public ActionResult InventoryReport()
        {
            return View();
        }

        public ActionResult MiscellaneousReport()
        {
            return View();
        }
        public ActionResult ExportingReport()
        {
            return View();
        }
        public ActionResult MyReports()
        {
            return View();
        }

        public ActionResult GenerateReport(string target)
        {
            ViewBag.edit = target;
            ViewBag.branches = _unitOfWork.StoreRepository.GetStores().Select(a => new SelectListItem{Text = a.Name,Value = a.Id.ToString()});
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductSaleReport(DateTime dateFrom,DateTime dateTo,int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateCategoriesSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductSizeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateComboSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductSizeByOrderTypeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateModifierSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductModifierSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateBranchSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateBranchTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateLocationSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateTableSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateAgentIncomeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateCashierIncomeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateWaiterIncomeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateDriverIncomeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateCustomerSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GeneratePaymentMethodReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GeneratePaymentMethodTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateOrderTypeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
     
    }
}