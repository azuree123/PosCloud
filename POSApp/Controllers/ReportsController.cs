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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId=ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData((int)user.StoreId, dateFrom, dateTo),
                    "SalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "Sales.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId,  storeid= (int)user.StoreId });
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
            return View(Mapper.Map<ReportLogViewModel[]>(_unitOfWork.ReportsLogRepository.GetReportsLogs(this.HttpContext.User.Identity.GetUserId()).OrderByDescending(a=>a.CreatedOn)));
        }
        public ActionResult MyReportsPreview(int reportId,int storeid)
        {
            return View(Mapper.Map<ReportLogViewModel>(_unitOfWork.ReportsLogRepository.GetReportsLog(reportId,storeid)));
        }
        public ActionResult GenerateReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            if (store.ClientId == null)
            {
                ViewBag.branches = new List<SelectListItem>(){new SelectListItem
                {
                    Text = store.Name, Value = store.Id.ToString(),Selected = true
                }};

            }
            else
            {
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            ViewBag.branches = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            }
            return View();
        }
        public ActionResult GenerateEmployeeReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            if (store.ClientId == null)
            {
                ViewBag.branches = new List<SelectListItem>(){new SelectListItem
                {
                    Text = store.Name, Value = store.Id.ToString(),Selected = true
                }};

            }
            else
            {
                var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
                ViewBag.branches = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            }
            ViewBag.designations = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            return View();
        }
        public ActionResult GeneratePurchasePerSupplierReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            if (store.ClientId == null)
            {
                ViewBag.branches = new List<SelectListItem>(){new SelectListItem
                {
                    Text = store.Name, Value = store.Id.ToString(),Selected = true
                }};

            }
            else
            {
                var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
                ViewBag.branches = clientStores.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            }
            ViewBag.suppliers = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductSaleReport(DateTime dateFrom,DateTime dateTo,int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period from: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                string filePath = Server.MapPath("~/Content/Reports/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = "ProductSalesReport" + "_" + this.HttpContext.User.Identity.GetUserId() + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ProductSales.rpt"));
                //rd.Subreports[0].SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(details, "ProductSalesReport"));

                rd.SetDataSource(_unitOfWork.ReportsRepository.GenerateProductSalesData((int)user.StoreId, dateFrom, dateTo));
                foreach (ReportDocument reportDocument in rd.Subreports)
                {
                    reportDocument.SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(details, "ProductSalesReport"));
                }
                rd.SetParameterValue("totalDiscount", _unitOfWork.ReportsRepository.GetProductSalesDiscount((int)user.StoreId, dateFrom, dateTo));
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath + fileName);
                //_unitOfWork.ReportsLogRepository.AddReportsLog(new ReportsLog
                //{
                //    Name = "ProductSalesReport",
                //    Path = fileName,
                //    Status = "Ready",
                //    Details = details,
                //    StoreId = (int)user.StoreId

                //});
                //_unitOfWork.Complete();

                var report = new ReportsLog
                {
                    Name = "ProductSalesReport",
                    Path = fileName,
                    Status = "Ready",
                    Details = details,
                    StoreId = (int)user.StoreId

                };
                _unitOfWork.ReportsLogRepository.AddReportsLog(report);
                _unitOfWork.Complete();
                return RedirectToAction("MyReportsPreview","Reports",new{ reportId = report.Id,storeId=report.StoreId});

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

           

            string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCategoriesSalesData((int)user.StoreId, dateFrom, dateTo),
                "CategoriesSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "CategoriesSales.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
            string details = "Period from: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                "ProductSizeWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductSizeWiseSales.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
            string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateComboSalesData((int)user.StoreId, dateFrom, dateTo),
                "ComboSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)user.StoreId, details, Server.MapPath("~/Reports"), "ComboSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeOrderTypeSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductSizeWiseOrderTypeSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductSizeOrderTypeSales.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateModifierSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ModifierSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductModifierSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductModifierSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductTimeWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "ProductTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductTimelySale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });


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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateBranchSalesData((int)user.StoreId, dateFrom, dateTo),
                    "BranchSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "BranchWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTimelyBranchSalesData((int)user.StoreId, dateFrom, dateTo),
                    "BranchTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "BranchTimelySale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });


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



                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "TableSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "TableWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateAgentIncomeReport(int branchId,int designationId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "BranchID: " + branchId.ToString() + "- DesignationId: " + designationId.ToString();
                int logId = ExcelService.GenerateEmployeeCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeIncomeData((int)user.StoreId, designationId),
                    "EmployeeIncomeReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId,details, Server.MapPath("~/Reports"), "EmployeeIncomeReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateCustomerSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomerWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "CustomerWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "CustomerSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "PaymentMethodWiseSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "PaymentMethodWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodTimeWiseSalesData((int)user.StoreId, dateFrom, dateTo),
                    "PaymentMethodTimeWiseSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "PaymentMethodTimeWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });


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
        public ActionResult GenerateOrderTypeSaleReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductOrderTypeSalesData((int)user.StoreId, dateFrom, dateTo),
                    "OrderTypeSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "OrderTypeWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateStockReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                var data = _unitOfWork.ReportsRepository.GenerateStockData((int) user.StoreId, dateFrom, dateTo);
                int logId = ExcelService.GenerateCrystalReport(data,
                    "ProductStockReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "Stock.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTotalPurchasesData((int)user.StoreId, dateFrom, dateTo),
                    "TotalPurchasesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "TotalPurchasesReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GeneratePendingTransferReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductRecipeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductRecipeData((int)user.StoreId, dateFrom, dateTo),
                    "ProductRecipeReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductRecipe.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GeneratePurchasePerSupplierReport(int branchId, int supplierId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "BranchID: " + branchId.ToString() + "- SupplierId: " + supplierId.ToString();
                int logId = ExcelService.GeneratePurchasePerSupplierCrystalReport(_unitOfWork.ReportsRepository.GeneratePurchasesPerSupplierData((int)user.StoreId, supplierId),
                    "PurchasePerSupplierReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "PurchasePerSupplierReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateProductPreparationTimeReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateProductCostReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductCostData((int)user.StoreId, dateFrom, dateTo),
                    "ProductsCostReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "ProductsCostReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period from: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTillOperationData((int)user.StoreId, dateFrom, dateTo),
                    "TilOperationsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "TilOperationsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateVoidReasonReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateEmployeeShiftReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeShiftData((int)user.StoreId, dateFrom, dateTo),
                    "EmployeesShiftReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "EmployeeShiftReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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

            string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
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
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomersData((int)user.StoreId, dateFrom, dateTo),
                    "CustomerReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "CustomerReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
        public ActionResult GenerateStockTakingReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateInventoryTransactionReport(DateTime dateFrom, DateTime dateTo, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string details = "Period From: " + dateFrom.ToShortDateString() + " To: " + dateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTransactionsData((int)user.StoreId, dateFrom, dateTo),
                    "TransactionsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)user.StoreId, details, Server.MapPath("~/Reports"), "TransactionsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)user.StoreId });
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
