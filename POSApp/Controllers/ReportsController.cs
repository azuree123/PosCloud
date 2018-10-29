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


        [HttpPost]
        public ActionResult GenerateItemCostReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateSemiFinishedItemCostReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateInventoryItemTotalCostReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateItemHistoryReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateInventoryControlReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateConsumptionReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateLevelReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateExpirationReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateTotalTransferReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateTotalPurchaseReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GeneratePendingTransferReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductRecipeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateInventoryItemRecipeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateModifierRecipeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GeneratePurchaseperSupplierReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
          
        }
        [HttpPost]
        public ActionResult GenerateProductPreparationTimeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateProductCostReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateProductReturnReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateTillLogReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateTillOperationReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateVoidReasonReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateEmployeeShiftReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateTaxReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateOrderDiscountReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateProductDiscountReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateProductSizeDiscountReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateDataReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateOrderReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateCouponReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateCustomerReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateStockTakingReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateInventoryTransactionReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GeneratePurchaseOrderReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateSnapshotReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
 

    }

    }
