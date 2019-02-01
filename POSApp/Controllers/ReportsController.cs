using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private ApplicationUserManager _userManager;
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
        [HttpPost]
        public ActionResult GenerateSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData((int)user.StoreId, dateFrom, dateTo),
                    "SalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "Sales.rpt");
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

            return RedirectToAction("MyReports");
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
            return View(Mapper.Map<ReportLogViewModel[]>(_unitOfWork.ReportsLogRepository.GetReportsLogs(this.HttpContext.User.Identity.GetUserId())));
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
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductSales.rpt");


             
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
            return RedirectToAction("MyReports");

        }
        [HttpPost]
        public ActionResult GenerateCategoriesSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

           

            string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
            ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCategoriesSalesData((int)user.StoreId, dateFrom, dateTo),
                "CategoriesSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "CategoriesSales.rpt");

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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateProductSizeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
            ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                "ProductSizeWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductSizeWiseSales.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateComboSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
            ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateComboSalesData((int)user.StoreId, dateFrom, dateTo),
                "ComboSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "ComboSale.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateProductSizeByOrderTypeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeOrderTypeSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductSizeWiseOrderTypeSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductSizeOrderTypeSales.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateModifierSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateModifierSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ModifierSale.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateProductModifierSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductModifierSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductModifierSale.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateProductTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductTimeWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductTimelySale.rpt");



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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateBranchSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateBranchSalesData((int)user.StoreId, dateFrom, dateTo),
                    "BranchSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "BranchWiseSale.rpt");
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

                return RedirectToAction("MyReports");
            
        }
        [HttpPost]
        public ActionResult GenerateBranchTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTimelyBranchSalesData((int)user.StoreId, dateFrom, dateTo),
                    "BranchTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "BranchTimelySale.rpt");



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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateLocationSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateTableSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);



                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "TableSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "TableWiseSale.rpt");

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
            return RedirectToAction("MyReports");
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
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomerWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "CustomerWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "CustomerSale.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GeneratePaymentMethodReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "PaymentMethodWiseSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "PaymentMethodWiseSale.rpt");
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GeneratePaymentMethodTimelySaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateOrderTypeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
                ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductOrderTypeSalesData((int)user.StoreId, dateFrom, dateTo),
                    "OrderTypeSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "OrderTypeWiseSale.rpt");
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

            return RedirectToAction("MyReports");
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

        public ActionResult DeleteReport(int id,int storeId)
        {
            try
            {

            _unitOfWork.ReportsLogRepository.DeleteReportsLog(id,storeId);
            _unitOfWork.Complete();
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateTaxReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

            string path = Server.MapPath("~/Content/Reports/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string details = "Date Range: " + dateFrom.ToShortDateString() + "-" + dateTo.ToShortDateString();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ExcelService.GenerateExcelSheet(ExcelService.ToDataTable(_unitOfWork.TaxRepository.GetTaxes(branchId)), "TaxReport", path, this.HttpContext.User.Identity.GetUserId(),_unitOfWork,(int)user.StoreId,details);
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
            return RedirectToAction("MyReports");

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
