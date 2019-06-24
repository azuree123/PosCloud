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
using POSApp.SecurityFilters;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class ReportsController : LanguageController
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
        //[Manage(Config.Reports.SalesReport)]
        public ActionResult GenerateSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "المبيعات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "المبيعات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "SalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "Sales.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
        public ActionResult GenerateShiftReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
        public ActionResult GenerateCostReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
        public ActionResult GenerateCustomerReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
            ViewBag.designations = _unitOfWork.DesignationRepository.GetDesignations((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            return View();
        }
        
        public ActionResult GeneratePurchasePerSupplierReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
            ViewBag.suppliers = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            return View();
        }
        
        public ActionResult GenerateStockReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var store = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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
        [HttpPost]
        //  [Manage(Config.Reports.ProductSaleReport)]

        public ActionResult GenerateProductSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    string filePath = Server.MapPath("~/Content/Reports/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = "مبيعات المنتجات" + "_" + this.HttpContext.User.Identity.GetUserId() + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "مبيعات المنتجات.rpt"));
                    //rd.Subreports[0].SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(details, "ProductSalesReport"));

                    rd.SetDataSource(_unitOfWork.ReportsRepository.GenerateProductSalesData(storeIds, gr.DateFrom, gr.DateTo));
                    foreach (ReportDocument reportDocument in rd.Subreports)
                    {
                        reportDocument.SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(branchId, details, "ProductSalesReport"));
                    }
                    rd.SetParameterValue("totalDiscount", _unitOfWork.ReportsRepository.GetProductSalesDiscount(storeIds, gr.DateFrom, gr.DateTo));
                    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath + fileName);
                    //_unitOfWork.ReportsLogRepository.AddReportsLog(new ReportsLog
                    //{
                    //    Name = "ProductSalesReport",
                    //    Path = fileName,
                    //    Status = "Ready",
                    //    Details = details,
                    //    StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)

                    //});
                    //_unitOfWork.Complete();

                    var report = new ReportsLog
                    {
                        Name = "ProductSalesReport",
                        Path = fileName,
                        Status = "Ready",
                        Details = details,
                        StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)

                    };
                    _unitOfWork.ReportsLogRepository.AddReportsLog(report);
                    _unitOfWork.Complete();
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = report.Id, storeId = report.StoreId });

                }
                else
                {
                    
                string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                string filePath = Server.MapPath("~/Content/Reports/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = "ProductSalesReport" + "_" + this.HttpContext.User.Identity.GetUserId() + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ProductSales.rpt"));
                //rd.Subreports[0].SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(details, "ProductSalesReport"));

                rd.SetDataSource(_unitOfWork.ReportsRepository.GenerateProductSalesData(storeIds, gr.DateFrom,gr.DateTo));
                foreach (ReportDocument reportDocument in rd.Subreports)
                {
                    reportDocument.SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(branchId, details, "ProductSalesReport"));
                }
                rd.SetParameterValue("totalDiscount", _unitOfWork.ReportsRepository.GetProductSalesDiscount(storeIds, gr.DateFrom, gr.DateTo));
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath + fileName);
                //_unitOfWork.ReportsLogRepository.AddReportsLog(new ReportsLog
                //{
                //    Name = "ProductSalesReport",
                //    Path = fileName,
                //    Status = "Ready",
                //    Details = details,
                //    StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)

                //});
                //_unitOfWork.Complete();

                var report = new ReportsLog
                {
                    Name = "ProductSalesReport",
                    Path = fileName,
                    Status = "Ready",
                    Details = details,
                    StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)

                };
                _unitOfWork.ReportsLogRepository.AddReportsLog(report);
                _unitOfWork.Complete();
                return RedirectToAction("MyReportsPreview","Reports",new{ reportId = report.Id,storeId=report.StoreId});


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
            return RedirectToAction("MyReports");

        }
        [HttpPost]
        //   [Manage(Config.Reports.CategorySaleReport)]

        public ActionResult GenerateCategoriesSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCategoriesSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "فئات المبيعات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "فئات المبيعات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCategoriesSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "CategoriesSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "CategoriesSales.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        // [Manage(Config.Reports.ProductSizeSaleReport)]

        public ActionResult GenerateProductSizeSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات أحجام المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات أحجام المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                  string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                  int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                      "ProductSizeWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                      (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductSizeWiseSales.rpt");
                  return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.ComboSaleReport)]

        public ActionResult GenerateComboSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateComboSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات الوجبات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الوجبات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateComboSalesData(storeIds, gr.DateFrom, gr.DateTo),
                "ComboSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ComboSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.ProductSizeByOrderTypeReport)]

        public ActionResult GenerateProductSizeByOrderTypeSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeOrderTypeSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات أحجام المنتجات بناء على نوعية الطلب", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات أحجام المنتجات بناء على نوعية الطلب.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductSizeOrderTypeSalesData(storeIds, gr.DateFrom, gr.DateTo),
                    "ProductSizeWiseOrderTypeSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductSizeOrderTypeSales.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.ModifierSaleReport)]

        public ActionResult GenerateModifierSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateModifierSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات الخيارات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الخيارات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateModifierSalesData(storeIds, gr.DateFrom, gr.DateTo),
                    "ModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ModifierSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]

        //  [Manage(Config.Reports.ProductModifierSaleReport)]

        public ActionResult GenerateProductModifierSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductModifierSalesData(storeIds, gr.DateFrom, gr.DateTo),
                    "مبيعات الخيارات بناء على المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الخيارات بناء على المنتجات.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductModifierSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "ProductModifierWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductModifierSale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        // [Manage(Config.Reports.ProductTimelySaleReport)]

        public ActionResult GenerateProductTimelySaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductTimeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات المنتجات بناء على الوقت", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات المنتجات بناء على الوقت.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {

                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductTimeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "ProductTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductTimelySale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.BranchSaleReport)]

        public ActionResult GenerateBranchSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateBranchSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات الفروع", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الفروع.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateBranchSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "BranchSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "BranchWiseSale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

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

                return RedirectToAction("MyReports");
            
        }
        [HttpPost]
        // [Manage(Config.Reports.BranchTimelySaleReport)]

        public ActionResult GenerateBranchTimelySaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();

                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTimelyBranchSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات الفروع بناء على الوقت", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الفروع بناء على الوقت.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " +
                                     gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTimelyBranchSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "BranchTimelySaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "BranchTimelySale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateLocationSaleReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]

//

        public ActionResult GenerateTableSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }


                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات الطاولات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الطاولات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData(storeIds,gr.DateFrom, gr.DateTo),
                    "TableSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TableWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]

        // [Manage(Config.Reports.EmployeesIncomeReport)]

        public ActionResult GenerateAgentIncomeReport(int branchId,int designationId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "BranchID: " + branchId.ToString() + "- DesignationId: " + designationId.ToString();
                    int logId = ExcelService.GenerateEmployeeCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeIncomeData(storeIds, designationId),
                        "دخل الموظفين", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "دخل الموظفين.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "BranchID: " + branchId.ToString() + "- DesignationId: " + designationId.ToString();
                    int logId = ExcelService.GenerateEmployeeCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeIncomeData(storeIds, designationId),
                        "EmployeeIncomeReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "EmployeeIncomeReport.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
      
        [HttpPost]
        //  [Manage(Config.Reports.CustomerSaleReport)]

        public ActionResult GenerateCustomerSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomerWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "مبيعات العملاء", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات العملاء.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomerWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                    "CustomerWiseSalesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "CustomerSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.PaymentMethodReport)]

        public ActionResult GeneratePaymentMethodReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {


                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "طرق الدفع", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "طرق الدفع.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "PaymentMethodWiseSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "PaymentMethodWiseSale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //  [Manage(Config.Reports.PaymentMethodTimelySaleReport)]

        public ActionResult GeneratePaymentMethodTimelySaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }


                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodTimeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "طرق الدفع بناء على الوقت", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "طرق الدفع بناء على الوقت.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePaymentMethodTimeWiseSalesData(storeIds, gr.DateFrom, gr.DateTo),
                        "PaymentMethodTimeWiseSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "PaymentMethodTimeWiseSale.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
            return RedirectToAction("MyReports");
        }
        [HttpPost]
        //   [Manage(Config.Reports.OrderTypeSaleReport)]

        public ActionResult GenerateOrderTypeSaleReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductOrderTypeSalesData(storeIds, gr.DateFrom, gr.DateTo),
                    "OrderTypeSaleReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "OrderTypeWiseSale.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
        public ActionResult GenerateItemCostReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        //  [Manage(Config.Reports.StockReport)]

        public ActionResult GenerateStockReport(int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }


                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " " + " ";
                    var data = _unitOfWork.ReportsRepository.GenerateStockData(storeIds);
                    int logId = ExcelService.GenerateCrystalReport(data,
                        "المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = " " + " ";
                    var data = _unitOfWork.ReportsRepository.GenerateStockData(storeIds);
                    int logId = ExcelService.GenerateCrystalReport(data,
                        "StockReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "Stock.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateSemiFinishedItemCostReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        //   [Manage(Config.Reports.InventoryItemsTotalCostReport)]

        public ActionResult GenerateInventoryItemTotalCostReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateItemsCostData(storeIds, gr.DateFrom, gr.DateTo),
                        "التكلفة الإجمالية للبضائع في المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "التكلفة الإجمالية للبضائع في المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateItemsCostData(storeIds, gr.DateFrom, gr.DateTo),
                    "InventoryTotalCostReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "InventoryTotalCostReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateItemHistoryReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateInventoryControlReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        //    [Manage(Config.Reports.ConsumptionReport)]

        public ActionResult GenerateConsumptionReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateConsumptionData(storeIds, gr.DateFrom, gr.DateTo),
                        "استهلاك", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "استهلاك.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
            string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateConsumptionData(storeIds, gr.DateFrom, gr.DateTo),
                    "ConsumptionReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ConsumptionReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateLevelReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        //  [Manage(Config.Reports.ExpirationReport)]

        public ActionResult BatchWiseExpiryReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.BatchWiseExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                        "دفعة الحكيم انتهاء", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "دفعة الحكيم انتهاء.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.BatchWiseExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                        "BatchWiseExpiry", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "BatchWiseExpiry.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }

        public ActionResult GenerateExpirationReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {


                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                        "انتهاء الصلاحية", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "انتهاء الصلاحية.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                    "ExpiredItemsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ExpiredItemsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateTotalTransferReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        // [Manage(Config.Reports.TotalPurchasesReport)]

        public ActionResult GenerateTotalPurchaseReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTotalPurchasesData(storeIds, gr.DateFrom, gr.DateTo),
                        "اجمالي المشتريات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "اجمالي المشتريات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTotalPurchasesData(storeIds, gr.DateFrom, gr.DateTo),
                        "TotalPurchasesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TotalPurchasesReport.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GeneratePendingTransferReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]

        //   [Manage(Config.Reports.ProductsRecipeReport)]

        public ActionResult GenerateProductRecipeReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductRecipeData(branchId, gr.DateFrom, gr.DateTo),
                        "وصفات المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "وصفات المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductRecipeData(branchId, gr.DateFrom, gr.DateTo),
                        "ProductRecipeReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductRecipe.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateInventoryItemRecipeReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateModifierRecipeReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
       [HttpPost]
       //  [Manage(Config.Reports.PurchasesPerSupplierReport)]

        public ActionResult GeneratePurchasePerSupplierReport(int branchId, int supplierId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }
                string details = "BranchID: " + branchId.ToString() + "- SupplierId: " + supplierId.ToString();

                if (Global.GetLang().Contains("Ar"))
                {
                    int logId = ExcelService.GeneratePurchasePerSupplierCrystalReport(_unitOfWork.ReportsRepository.GeneratePurchasesPerSupplierData(storeIds, supplierId),
                        "المشتريات لكل مورد", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "المشتريات لكل مورد.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    int logId = ExcelService.GeneratePurchasePerSupplierCrystalReport(_unitOfWork.ReportsRepository.GeneratePurchasesPerSupplierData(storeIds, supplierId),
                        "PurchasePerSupplierReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "PurchasePerSupplierReport.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");
        }
        [HttpPost]
        public ActionResult GenerateProductPreparationTimeReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        //  [Manage(Config.Reports.ProductsCostReport)]

        public ActionResult GenerateProductCostReport(int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " " + " ";
                    int logId = ExcelService.GenerateCostCrystalReport(_unitOfWork.ReportsRepository.GenerateProductCostData(storeIds),
                        "تكلفة المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "تكلفة المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {

                string details = " " +  " ";
                int logId = ExcelService.GenerateCostCrystalReport(_unitOfWork.ReportsRepository.GenerateProductCostData(storeIds),
                    "ProductsCostReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductsCostReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]
        // [Manage(Config.Reports.ProductReturnsReport)]

        public ActionResult GenerateProductReturnReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateReturnData(storeIds, gr.DateFrom, gr.DateTo),
                        "إرجاع المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "إرجاع المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

                }
                else
                {
                    
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateReturnData(storeIds, gr.DateFrom, gr.DateTo),
                    "ProductReturnReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductReturnReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]
        public ActionResult GenerateTillLogReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]

        // [Manage(Config.Reports.TillOperationsReport)]

        public ActionResult GenerateTillOperationReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTillOperationData(storeIds, gr.DateFrom, gr.DateTo),
                        "عمليات صندوق النقد", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "عمليات صندوق النقد.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTillOperationData(storeIds, gr.DateFrom, gr.DateTo),
                    "TilOperationsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TilOperationsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]
        //  [Manage(Config.Reports.VoidReasonsReport)]

        public ActionResult GenerateVoidReasonReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateVoidReasonsData(storeIds, gr.DateFrom, gr.DateTo),
                        "أسباب إلغاء الطلبات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "أسباب إلغاء الطلبات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period from: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateVoidReasonsData(storeIds, gr.DateFrom, gr.DateTo),
                    "VoidReasonsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "VoidReasonsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]
        //  [Manage(Config.Reports.EmployeeShiftsReport)]

        public ActionResult GenerateEmployeeShiftReport(int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " " + " To: ";
                    int logId = ExcelService.GenerateShiftCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeShiftData(storeIds),
                        "فترة عمل الموظفين", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "فترة عمل الموظفين.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = " " + " To: ";
                int logId = ExcelService.GenerateShiftCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeShiftData(storeIds),
                    "EmployeesShiftReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "EmployeeShiftReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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
//

        public ActionResult GenerateTaxReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTaxesData(storeIds, gr.DateFrom, gr.DateTo),
                        "الضرائب", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "الضرائب.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTaxesData(storeIds, gr.DateFrom, gr.DateTo),
                    "TaxesReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TaxesReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]

        //  [Manage(Config.Reports.OrderDiscountsReport)]

        public ActionResult GenerateOrderDiscountReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateOrderDiscountData(storeIds, gr.DateFrom, gr.DateTo),
                        "خصومات الطلبات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "خصومات الطلبات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateOrderDiscountData(storeIds, gr.DateFrom, gr.DateTo),
                    "OrderDiscountReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "OrderDiscountReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        [HttpPost]
        // [Manage(Config.Reports.ProductDiscountsReport)]

        public ActionResult GenerateProductDiscountReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductDiscountData(storeIds, gr.DateFrom, gr.DateTo),
                        "خصومات المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "خصومات المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductDiscountData(storeIds, gr.DateFrom, gr.DateTo),
                    "ProductsDiscountReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductsDiscountReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");

        }

        [HttpPost]
        //  [Manage(Config.Reports.TotalTransfersReport)]

        public ActionResult GenerateTransferReport( GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTransferData(storeIds, gr.DateFrom, gr.DateTo),
                        "التحويلات الإجمالية", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "التحويلات الإجمالية.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });

                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                   int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTransferData(storeIds, gr.DateFrom, gr.DateTo),
                       "TransferReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                       (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TransferReport.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");
        }



        [HttpPost]
        public ActionResult GenerateProductSizeDiscountReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateDataReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateOrderReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateCouponReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        // [Manage(Config.Reports.CustomerReports)]

        public ActionResult GenerateCustomerReport(int branchId)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {

                    string details = " " + " ";
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomersData(storeIds),
                        "تقرير العملاء", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "تقرير العملاء.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {

                string details = " " + " ";
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateCustomersData(storeIds),
                    "CustomerReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "CustomerReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }
        //[HttpPost]
        //public ActionResult GenerateStockTakingReport(GenerateReportViewModel gr, int branchId)
        //{
        //    return View();

        //}
        [HttpPost]
        //  [Manage(Config.Reports.InventoryTransactionsReports)]

        public ActionResult GenerateInventoryTransactionReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTransactionsData(storeIds, gr.DateFrom, gr.DateTo),
                        "العمليات على المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "العمليات على المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                 
                }
                else
                {
                    
                string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTransactionsData(storeIds, gr.DateFrom, gr.DateTo),
                    "TransactionsReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TransactionsReport.rpt");
                return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
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

            return RedirectToAction("MyReports");

        }


        [HttpPost]
        //  [Manage(Config.Reports.StockTakingReport)]

        public ActionResult GenerateStockTakingReport(GenerateReportViewModel gr, int branchId)
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> storeIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.StoreRepository.GetStoreById(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    storeIds.AddRange(_unitOfWork.ClientRepository.GetClientStore(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    storeIds.Add(branchId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateStockTakingData(storeIds, gr.DateFrom, gr.DateTo),
                        "جرد المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "جرد المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateStockTakingData(storeIds, gr.DateFrom, gr.DateTo),
                        "StockTakingReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "StockTakingReport.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                    
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

            return RedirectToAction("MyReports");
        }


        [HttpPost]
        public ActionResult GeneratePurchaseOrderReport(GenerateReportViewModel gr, int branchId)
        {
            return View();

        }
        [HttpPost]
        public ActionResult GenerateSnapshotReport(GenerateReportViewModel gr, int branchId)
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
