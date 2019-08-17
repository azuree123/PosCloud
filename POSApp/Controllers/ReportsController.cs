using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using System.Linq.Dynamic;
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
       public ActionResult SaleReport()
       {
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
            ViewBag.employees = _unitOfWork.EmployeeRepository.GetEmployees(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            ViewBag.shifts = _unitOfWork.ShiftRepository.GetShifts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name });

            return View();
        }

        public ActionResult GenerateTableReport(string target)
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
            ViewBag.tables = _unitOfWork.DineTableRepository.GetDineTables(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.DineTableNumber });
            
            return View();
        }
        public ActionResult GenerateSalesReport(string target)
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
            ViewBag.customers = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("C",UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            ViewBag.ordertypes = _unitOfWork.TransMasterRepository.GetTransMasters(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>a.DeliveryType != null).DistinctBy(a => a.DeliveryType).Select(a => new SelectListItem { Value = a.DeliveryType, Text = a.DeliveryType });
            ViewBag.paymentmethods = _unitOfWork.TransMasterPaymentMethodRepository
                .GetTransMasterPaymentMethods(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).DistinctBy(a=>a.Method.ToUpper())
                .Select(a => new SelectListItem {Value = a.Method, Text = a.Method});
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
            ViewBag.products = _unitOfWork.ProductRepository.GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>!a.InventoryItem && a.Type != "Combo").Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name });

            return View();
        }


        public ActionResult GeneratePurchaseReport(string target)
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

        public ActionResult GenerateRecipeReport(string target)
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
            ViewBag.products = _unitOfWork.ProductRepository.GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => !a.InventoryItem && a.Type != "Combo").Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name });

            return View();
        }
        [HttpPost]
        //[Manage(Config.Reports.SalesReport)]
        public ActionResult GenerateSaleReport(GenerateReportViewModel gr, int branchId,string methodId,int customerId, string orderType = "")
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
                List<int> customers = new List<int>();
                if (customerId == 0)
                {



                    customers.AddRange(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("C", UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Id).ToList());
                }
                else
                {
                    customers.Add(customerId);


                }
                List<string> methods = new List<string>();
                if (string.IsNullOrWhiteSpace(methodId))
                {



                    methods.AddRange(_unitOfWork.TransMasterPaymentMethodRepository.GetTransMasterPaymentMethods(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).DistinctBy(a => a.Method.ToUpper()).Select(a => a.Method).ToList());
                }
                else
                {
                    methods.Add(methodId);


                }
                List<string> orderTypes = new List<string>();
                if (string.IsNullOrWhiteSpace(orderType))
                {



                    orderTypes.AddRange(_unitOfWork.TransMasterRepository.GetTransMasters(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).DistinctBy(a=>a.DeliveryType).Where(a=>a.DeliveryType != null).Select(a => a.DeliveryType).ToList());
                }
                else
                {
                    orderTypes.Add(orderType);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData(storeIds,methods,orderTypes,customers, gr.DateFrom, gr.DateTo),
                        "المبيعات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "المبيعات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateSalesData(storeIds, methods, orderTypes, customers, gr.DateFrom, gr.DateTo),
                        "Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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

      
        [HttpPost]
        public ActionResult GenerateWarehouseStockReport(int branchId, string productCode = " ")
        {
            try
            {

                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<int> warehouseIds = new List<int>();
                if (branchId == 0)
                {
                    int clientId = _unitOfWork.WarehouseRepository.GetWarehouse(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).ClientId;


                    warehouseIds.AddRange(_unitOfWork.ClientRepository.GetClientWarehouse(clientId).Select(a => a.Id).ToList());
                }
                else
                {
                    warehouseIds.Add(branchId);


                }
                List<string> productCodes = new List<string>();
                if (string.IsNullOrWhiteSpace(productCode))
                {


                    productCodes.AddRange(_unitOfWork.ProductRepository
                        .GetInventoryProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                        .Select(a => a.ProductCode).ToList());
                }
                else
                {
                    productCodes.Add(productCode);


                }

                string details = " ";
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateWarehouseStock(warehouseIds,productCodes),
                    "Warehouse Stock Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "WarehouseStockReport.rpt");
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
        public ActionResult GenerateProductSaleReport(string target)
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
            ViewBag.categories = _unitOfWork.ProductCategoryRepository.GetProductCategories(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>a.Type != "Combo").Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            ViewBag.sizes = _unitOfWork.ProductRepository.GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).DistinctBy(a => a.Size).Where(a=>!a.InventoryItem).Select(a => new SelectListItem { Value = a.Size, Text = a.Size });
            return View();
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
        public ActionResult GenerateWarehouseStockReport(string target)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = target;
            var warehouse = _unitOfWork.WarehouseRepository.GetWarehouse((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            if (warehouse.ClientId == null)
            {
                ViewBag.warehouses = new List<SelectListItem>(){new SelectListItem
                {
                    Text = warehouse.Name, Value = warehouse.Id.ToString(),Selected = true
                }};

            }
            else
            {
                var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);
                ViewBag.warehouses = clientWarehouses.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            }
            ViewBag.products = _unitOfWork.ProductRepository.GetInventoryProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name });

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
        public ActionResult GenerateInventoryItemCostReport(string target)
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
            ViewBag.products = _unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>a.InventoryItem).Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name });
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
            ViewBag.products = _unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Value = a.ProductCode, Text = a.Name });
            return View();
        }
        [HttpPost]
        //  [Manage(Config.Reports.ProductSaleReport)]

        public ActionResult GenerateProductSaleReport(GenerateReportViewModel gr, int branchId, int categoryId,string sizeId="")
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
                List<int> categoryIds = new List<int>();
                if (categoryId == 0)
                {

                    categoryIds.AddRange(_unitOfWork.ProductCategoryRepository.GetProductCategories(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Id).ToList());
                }
                else
                {
                    categoryIds.Add(categoryId);


                }
                List<string> sizes = new List<string>();
                if (string.IsNullOrWhiteSpace(sizeId))
                {

                    sizes.AddRange(_unitOfWork.ProductRepository.GetProductsNotInventory(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Size).ToList());
                }
                else
                {
                    sizes.Add(sizeId);


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

                    rd.SetDataSource(_unitOfWork.ReportsRepository.GenerateProductSalesData(categoryIds, sizes,storeIds, gr.DateFrom, gr.DateTo));
                    foreach (ReportDocument reportDocument in rd.Subreports)
                    {
                        reportDocument.SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(branchId, details, "Product Sales Report"));
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
                        Name = "Product Sales Report",
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

                string fileName = "Product Sales Report" + "_" + this.HttpContext.User.Identity.GetUserId() + "_" + DateTime.Now.ToString("ddd, dd MMM yyy HH-mm-ss ") + ".PDF";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ProductSales.rpt"));
                //rd.Subreports[0].SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(details, "ProductSalesReport"));

                rd.SetDataSource(_unitOfWork.ReportsRepository.GenerateProductSalesData(categoryIds, sizes, storeIds, gr.DateFrom, gr.DateTo));
                foreach (ReportDocument reportDocument in rd.Subreports)
                {
                    reportDocument.SetDataSource(_unitOfWork.ReportsRepository.GenerateSubReportData(branchId, details, "Product Sales Report"));
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
                    Name = "Product Sales Report",
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
                        "Categories Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                      "Product Size Wise Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                "Combo Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Product Size Wise OrderType Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Modifier Wise Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Product Modifier Wise Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Product Timely Sale Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Branch Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Branch Timely Sale Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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

        public ActionResult GenerateTableSaleReport(GenerateReportViewModel gr, int branchId, int tableId)
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
                List<int> tableIds = new List<int>();
                if (tableId == 0)
                {
                   tableIds.AddRange(_unitOfWork.DineTableRepository.GetDineTables(
                        UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a=>a.Id).ToList());
                }
                else
                {
                    storeIds.Add(tableId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData(storeIds,tableIds ,gr.DateFrom, gr.DateTo),
                        "مبيعات الطاولات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "مبيعات الطاولات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateTableWiseSalesData(storeIds,tableIds,gr.DateFrom, gr.DateTo),
                    "Table Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                List<int> designationIds = new List<int>();
                if (designationId == 0)
                {



                    designationIds.AddRange(_unitOfWork.EmployeeRepository.GetEmployees(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Id).ToList());
                }
                else
                {
                    designationIds.Add(designationId);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " ";
                    int logId = ExcelService.GenerateEmployeeCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeIncomeData(storeIds, designationIds),
                        "دخل الموظفين", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "دخل الموظفين.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = " " ;
                    int logId = ExcelService.GenerateEmployeeCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeIncomeData(storeIds, designationIds),
                        "Employee Income Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Customer Wise Sales Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Payment Method Wise Sale Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Payment Method Time Wise Sale Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Order Type Sale Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
        //  [Manage(Config.Reports.StockReport)]

        public ActionResult GenerateStockReport(int branchId = 0, string productCode = " ")
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
                List<string> productCodes = new List<string>();
                if (string.IsNullOrWhiteSpace(productCode))
                {


                    productCodes.AddRange(_unitOfWork.ProductRepository
                        .GetInventoryProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                        .Select(a => a.ProductCode).ToList());
                }
                else
                {
                    productCodes.Add(productCode);


                }

                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " " + " ";
                    var data = _unitOfWork.ReportsRepository.GenerateStockData(storeIds,productCodes);
                    int logId = ExcelService.GenerateCrystalReport(data,
                        "المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = " " + " ";
                    var data = _unitOfWork.ReportsRepository.GenerateStockData(storeIds, productCodes);
                    int logId = ExcelService.GenerateCrystalReport(data,
                        "Stock Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
        //   [Manage(Config.Reports.InventoryItemsTotalCostReport)]

        public ActionResult GenerateInventoryItemTotalCostReport(GenerateReportViewModel gr, int branchId,string productCode)
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
                List<string> productcodes = new List<string>();
                if (string.IsNullOrWhiteSpace(productCode))
                {
                    productcodes.AddRange(_unitOfWork.ProductRepository.GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => a.ProductCode).ToList());
                }
                else
                {
                    productcodes.Add(productCode);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateItemsCostData(storeIds, productcodes,gr.DateFrom, gr.DateTo),
                        "التكلفة الإجمالية للبضائع في المخزون", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "التكلفة الإجمالية للبضائع في المخزون.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                string details = "Period From: " +gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateItemsCostData(storeIds, productcodes, gr.DateFrom, gr.DateTo),
                    "Inventory Total Cost Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Consumption Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Batch Wise Expiry", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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

        [HttpPost]
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
                    string details = "من تاريخ: " + gr.DateFrom.ToShortDateString() + " إلى: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                        "انتهاء الصلاحية", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)user.StoreId, details, Server.MapPath("~/Reports"), "انتهاء الصلاحية.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = "Period From: " + gr.DateFrom.ToShortDateString() + " To: " + gr.DateTo.ToShortDateString();
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateExpiryData(storeIds, gr.DateFrom, gr.DateTo),
                        "Expired Items Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)user.StoreId, details, Server.MapPath("~/Reports"), "ExpiredItemsReport.rpt");
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
        public ActionResult GeneratePurchaseOrderReport(DateTime dateFrom, DateTime dateTo, int branchId, int supplierId)
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
                List<int> supplierIds = new List<int>();
                if (supplierId == 0)
                {

                    supplierIds.AddRange(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Id).ToList());
                }
                else
                {
                    supplierIds.Add(supplierId);


                }
                string details = " ";
                int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GeneratePurchaseOrderData(storeIds, dateFrom, dateTo, supplierIds),
                    "ToPurchaseOrder Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "TotalPurchasesReport.rpt");
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
        public ActionResult GeneratePendingTransferReport(GenerateReportViewModel gr, int branchId)
        {
            return View();
        }
        [HttpPost]

        //   [Manage(Config.Reports.ProductsRecipeReport)]

        public ActionResult GenerateProductRecipeReport(int branchId, string productCode)
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
                List<string> productCodes = new List<string>();
                if (string.IsNullOrWhiteSpace(productCode))
                {


                    productCodes.AddRange(_unitOfWork.ProductRepository
                        .GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => !a.InventoryItem && a.Type != "Combo")
                        .Select(a => a.ProductCode).ToList());
                }
                else
                {
                    productCodes.Add(productCode);


                }
                if (Global.GetLang().Contains("Ar"))
                {
                    string details = " ";
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductRecipeData(storeIds,productCodes),
                        "وصفات المنتجات", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                        (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "وصفات المنتجات.rpt");
                    return RedirectToAction("MyReportsPreview", "Reports", new { reportId = logId, storeid = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current) });
                }
                else
                {
                    string details = " ";
                    int logId = ExcelService.GenerateCrystalReport(_unitOfWork.ReportsRepository.GenerateProductRecipeData(storeIds, productCodes),
                        "Product Recipe Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                string details = " ";

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
                        "Purchase Per Supplier Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
        public ActionResult GenerateProductCostReport(int branchId = 0, string productCode = "")
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
                List<string> productCodes = new List<string>();
                if (string.IsNullOrWhiteSpace(productCode))
                {


                    productCodes.AddRange(_unitOfWork.ProductRepository
                        .GetAllProducts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>!a.InventoryItem && a.Type != "Combo")
                        .Select(a => a.ProductCode).ToList());
                }
                else
                {
                    productCodes.Add(productCode);


                }
                string details = " " + " ";
                int logId = ExcelService.GenerateCostCrystalReport(_unitOfWork.ReportsRepository.GenerateProductCostData(storeIds, productCodes),
                    "ProductsCostReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "ProductsCostReport.rpt");
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
                    "Product Return Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "TilOperations Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Void Reasons Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
        public ActionResult GenerateEmployeeShiftReport(int branchId, int employeeId, int shiftId)
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
                List<int> employeeIds = new List<int>();
                if (employeeId == 0)
                {

                    employeeIds.AddRange(_unitOfWork.EmployeeRepository.GetEmployees(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.Id).ToList());
                }
                else
                {
                    employeeIds.Add(employeeId);


                }
                List<int> shiftIds = new List<int>();
                if (employeeId == 0)
                {

                    shiftIds.AddRange(_unitOfWork.ShiftRepository.GetShifts(UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => a.ShiftId).ToList());
                }
                else
                {
                    shiftIds.Add(shiftId);


                }
                string details = " ";
                int logId = ExcelService.GenerateShiftCrystalReport(_unitOfWork.ReportsRepository.GenerateEmployeeShiftData(storeIds, employeeIds, shiftIds),
                    "EmployeesShiftReport", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
                    (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), details, Server.MapPath("~/Reports"), "EmployeeShiftReport.rpt");
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
                    "Taxes Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Order Discount Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Products Discount Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                       "Transfer Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Customer Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                    "Transactions Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
                        "Stock Taking Report", this.HttpContext.User.Identity.GetUserId(), _unitOfWork,
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
