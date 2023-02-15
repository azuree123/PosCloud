using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.Shared;
using POSApp.Core.ViewModels;
using POSApp.SecurityFilters;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class PurchaseOrdersController : LanguageController
    {
        private IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;
        public PurchaseOrdersController()
        {

        }

        public PurchaseOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PurchaseOrders
        [View(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult PurchaseOrderList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "PRO").OrderByDescending(a => a. Id)));
        }
        [View(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult DailyPurchaseOrderList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMastersByDate((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "PRI").OrderByDescending(a => a.Id)));
        }

        [View(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult PreviewPurchaseOrder(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels = 
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
        return RedirectToAction("GenerateReceipt","PurchaseOrders");

    }

        [View(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult PurchaseOrderDetailList(int purchaseOrderId)
    {
        return View();
    }
        [Manage(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult AddPurchaseOrder()
        {
            TransMasterViewModel po=new TransMasterViewModel();
            po.Type = "PRO";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S",(int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem{Value = a.Id.ToString(),Text = a.Name});
            var warehouse = _unitOfWork.WarehouseRepository.GetWarehouse((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);

            po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            //po.WarehouseDdl = _unitOfWork.WarehouseRepository.GetWarehouses().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name, Selected = a.Id == a.Id });

            if (PoHelper.temptTransDetail != null)
            {
                
                PoHelper.EmptyTemptTransDetail(user.Id,(int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult AddPurchaseOrder(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "PRO";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                var warehouse = _unitOfWork.WarehouseRepository.GetWarehouse((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);

                po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

                return View(po);
            }
            else
            {
               
                int TransId = _unitOfWork.AppCountersRepository.GetId("Purchase");
                po.TransCode = "PRO-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                
                var savePo = Mapper.Map<TransMaster>(po);
                
                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a=>a.CreatedByUserId==userid && a.StoreId==UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity  * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
                return RedirectToAction("GenerateReceipt","PurchaseOrders");
            

        }
        [Manage(Config.PurchaseOrders.PurchaseOrder)]


        public ActionResult AddTransactionItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a=>(a.PurchaseItem||a.InventoryItem)).OrderBy(a=>a.Name).Select(a=>new SelectListItem{Text = a.Name+"("+a.ProductCode+")",Value = a.Id.ToString()}));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult AddTransactionItem(int productId,int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost, string batchNumber, DateTime? manufactureDate, DateTime? expiryDate)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail=new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity,cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        [Manage(Config.PurchaseOrders.PurchaseOrder)]

        public ActionResult RemoveTransactionItem(string productId)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
                return View("PoTable", PoHelper.temptTransDetail);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            PoHelper.RemoveFromTemptTransDetail(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), user.Id);
            return View("PoTable",PoHelper.temptTransDetail);
        }

        //Stock
        [View(Config.PurchaseOrders.Stock)]

        public ActionResult PreviewStock(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.WarehouseViewModel =
                Mapper.Map<WarehouseViewModel>(_unitOfWork.WarehouseRepository.GetWarehouse((int)temp.TransMasterViewModel.WarehouseId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateStockReceipt", "PurchaseOrders");

        }
        [View(Config.PurchaseOrders.Stock)]

        public ActionResult StockList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "STI").OrderByDescending(a => a.Id)));
        }
        [Manage(Config.PurchaseOrders.Stock)]

        public ActionResult AddStock()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "STI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var warehouse = _unitOfWork.WarehouseRepository.GetWarehouse((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);

            po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Stock)]

        public ActionResult AddStock(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "STI";
            if (!ModelState.IsValid)
            {
                var warehouse = _unitOfWork.WarehouseRepository.GetWarehouse((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);

                po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("StockIn");
                po.TransCode = "STI-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.WarehouseViewModel =
                    Mapper.Map<WarehouseViewModel>(_unitOfWork.WarehouseRepository.GetWarehouse((int)po.WarehouseId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("StockList", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.Stock)]

        public ActionResult AddStockItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Stock)]

        public ActionResult AddStockItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        [Manage(Config.PurchaseOrders.Stock)]

        public ActionResult DeleteStock(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Stock deleted successfully", AlertType.Success);
                return RedirectToAction("StockList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("StockList");

        }
        //Transfer
        [View(Config.PurchaseOrders.Transfer)]

        public ActionResult PreviewTransferOrder(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateTransferReceipt", "PurchaseOrders");

        }

        [View(Config.PurchaseOrders.Transfer)]

        public ActionResult TransferList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "TRS").OrderByDescending(a => a.Id)));
        }
        [Manage(Config.PurchaseOrders.Transfer)]

        public ActionResult AddTransfer()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "TRS";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

           
            po.StoreId = (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current);

            
            
            
            var store = _unitOfWork.StoreRepository.GetStoreById((int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int) store.ClientId);

            po.StoreDdl = clientStores.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
           
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Transfer)]

        public ActionResult AddTransfer(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "TRS";
            if (!ModelState.IsValid)
            {
                
                po.StoreDdl = _unitOfWork.StoreRepository.GetStores().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Transfer");
                po.TransCode = "TRS-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateTransferReceipt", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.Transfer)]

        public ActionResult AddTransferItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Transfer)]

        public ActionResult AddTransferItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        [Manage(Config.PurchaseOrders.Transfer)]

        public ActionResult DeleteTransfer(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Transfer deleted successfully", AlertType.Success);
                return RedirectToAction("TransferList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("TransferList");

        }
        //Purchasing

        [View(Config.PurchaseOrders.Purchasing)]

        public ActionResult PreviewPurchasing(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GeneratePurchasingReceipt", "PurchaseOrders");

        }
        [View(Config.PurchaseOrders.Purchasing)]

        public ActionResult PurchasingList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "PRI").OrderByDescending(a => a.Id)));
        }
       
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult AddPurchasing()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "PRI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int) UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.TransCode});
            
            
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            var warehouse = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);
            po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();

            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult AddPurchasing(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "PRI";
            if (!ModelState.IsValid)
            {
                po.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.TransCode });
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                var warehouse = _unitOfWork.StoreRepository.GetStoreById((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                var clientWarehouses = _unitOfWork.ClientRepository.GetClientWarehouse((int)warehouse.ClientId);
                po.WarehouseDdl = clientWarehouses.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).AsEnumerable();

                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Purchasing");
                po.TransCode = "PRI-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);
                savePo.TransDetails = Mapper.Map<List<TransDetail>>(po.TransDetailViewModels);
                IEnumerable<TransDetailViewModel> poItems = po.TransDetailViewModels;
                
                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
              
                TempData["po"] = temp;
            }
            return RedirectToAction("GeneratePurchasingReceipt", "PurchaseOrders");



        }
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult AddPurchasingItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult AddPurchasingItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity ,decimal cost)
        {
            TransMasterViewModel po=new TransMasterViewModel();
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }

 
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            
            
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            po.TransDetailViewModels = PoHelper.temptTransDetail;
            return View("PoPurchasing", po);
        }
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult RemovePurchasingItem(string productId)
        {
            TransMasterViewModel po = new TransMasterViewModel();

            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
                po.TransDetailViewModels = PoHelper.temptTransDetail;
                return View("PoPurchasing", po);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            PoHelper.RemoveFromTemptTransDetail(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), user.Id);
            po.TransDetailViewModels = PoHelper.temptTransDetail;
            return View("PoPurchasing", po);

        }

        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult UpdatePurchasing(int id)
        {

            
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
           
            TransMasterViewModel productVm = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))));
            productVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();

            }
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }

            productVm.TransDetailViewModels = _unitOfWork.TransDetailRepository
                .GetTransDetails(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))).ToList();

            foreach (var modifierVmModifierOptionViewModel in productVm.TransDetailViewModels)
            {
                PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductByCode(modifierVmModifierOptionViewModel.ProductCode,modifierVmModifierOptionViewModel.StoreId),modifierVmModifierOptionViewModel.Quantity,modifierVmModifierOptionViewModel.UnitPrice, userid);
            }

            productVm.TransDetailViewModels = PoHelper.temptTransDetail;
            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddPurchasing", productVm);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult UpdatePurchasing(int id, TransMasterViewModel purchasingVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            purchasingVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddPurchasing", purchasingVm);

                }
                else 

                {
                    TransMaster transMaster = Mapper.Map<TransMaster>(purchasingVm);
                    transMaster.Type = "PRI";
                    IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                    transMaster.TotalPrice = (from a in poItems
                        select a.Quantity * a.UnitPrice).Sum();
                    transMaster.TransDetails=new List<TransDetail>();
                    foreach (var productSubViewModel in poItems)
                    {
                        productSubViewModel.StoreId = transMaster.StoreId;
                        productSubViewModel.TransMasterId = transMaster.Id;
                        transMaster.TransDetails.Add(Mapper.Map<TransDetail>(productSubViewModel));

                    }
                    _unitOfWork.TransMasterRepository.UpdatePurchasing(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)),transMaster);

                    _unitOfWork.Complete();
                   
                    

                    TempData["Alert"] = new AlertModel("The Purchasing updated successfully", AlertType.Success);
                    return RedirectToAction("PurchasingList", "PurchaseOrders");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddPurchasing", purchasingVm);



        }
        [Manage(Config.PurchaseOrders.Purchasing)]

        public ActionResult DeletePurchasing(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Purchasing deleted successfully", AlertType.Success);
                return RedirectToAction("PurchasingList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("PurchasingList");

        }
        //End Purchasing
        [View(Config.PurchaseOrders.Purchasing)]

        public ActionResult GetPurchaseOrder(int id)
        {

            TransMasterViewModel productVm = new TransMasterViewModel();
            
            productVm.Type = "PRI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TransMasterViewModel temp = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))));
            productVm.BusinessPartnerId = temp.BusinessPartnerId;
            productVm.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current))
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.TransCode });
            productVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            productVm.WarehouseDdl = _unitOfWork.WarehouseRepository.GetWarehouses().OrderByDescending(a => a.Id).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name, Selected = a.Id == a.Id });
            productVm.IsPurchased = true;

            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();

            }
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }

            productVm.TransDetailViewModels = _unitOfWork.TransDetailRepository
                .GetTransDetails(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))).ToList();
            foreach (var modifierVmModifierOptionViewModel in productVm.TransDetailViewModels)
            {
                PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductByCode(modifierVmModifierOptionViewModel.ProductCode, modifierVmModifierOptionViewModel.StoreId), modifierVmModifierOptionViewModel.Quantity, modifierVmModifierOptionViewModel.UnitPrice, userid);
            }

            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddPurchasing", productVm);
        }
        [HttpPost]
        [View(Config.PurchaseOrders.Purchasing)]

        public ActionResult GetPurchaseOrder(int id, TransMasterViewModel purchasingVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            purchasingVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            purchasingVm.WarehouseDdl = _unitOfWork.WarehouseRepository.GetWarehouses().OrderByDescending(a => a.Id).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name, Selected = a.Id == a.Id });

            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            try
            {

                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddPurchasing", purchasingVm);

                }
                else

                {
                    int TransId = _unitOfWork.AppCountersRepository.GetId("Purchasing");
                    purchasingVm.TransCode = "PRI-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                    purchasingVm.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                    TransMaster temporary =_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                    temporary.IsPurchased = true;
                    var savePo = Mapper.Map<TransMaster>(purchasingVm);
                    savePo.TransDetails = Mapper.Map<List<TransDetail>>(purchasingVm.TransDetailViewModels);
                    savePo.Type = "PRI";
                    IEnumerable<TransDetailViewModel> poItems = purchasingVm.TransDetailViewModels;

                    savePo.TotalPrice = (from a in poItems
                        select a.Quantity * a.UnitPrice).Sum();
                    _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                    _unitOfWork.Complete();
                    
                    
                    temp.TransMasterViewModel = purchasingVm;
                    temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                    temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                    temp.BusinessPartnerViewModel =
                        Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)purchasingVm.BusinessPartnerId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                    temp.TransDetailViewModels = poItems;
                    temp.TotalAmount = (from a in temp.TransDetailViewModels
                        select a.Quantity * a.UnitPrice).Sum();
                    TempData["po"] = temp;
                }
                return RedirectToAction("GeneratePurchasingReceipt", "PurchaseOrders");

            
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddPurchasing", purchasingVm);



        }

        //Other In
        [View(Config.PurchaseOrders.OtherIn)]

        public ActionResult OtherInList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "OTI").OrderByDescending(a => a.Id)));
        }
        [View(Config.PurchaseOrders.OtherIn)]

        public ActionResult PreviewOtherIn(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateOtherInReceipt", "PurchaseOrders");

        }


        [Manage(Config.PurchaseOrders.OtherIn)]

        public ActionResult AddOtherIn()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OTI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.OtherIn)]

        public ActionResult AddOtherIn(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "OTI";
            if (!ModelState.IsValid)
            {
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("OtherIn");
                po.TransCode = "OTI-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateOtherInReceipt", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.OtherIn)]

        public ActionResult AddOtherInItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.OtherIn)]

        public ActionResult AddOtherInItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        [Manage(Config.PurchaseOrders.OtherIn)]

        public ActionResult DeleteOtherIn(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The OtherIn deleted successfully", AlertType.Success);
                return RedirectToAction("OtherInList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("OtherInList");

        }

        //Other Out
        [View(Config.PurchaseOrders.OtherOut)]

        public ActionResult OtherOutList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "OTO").OrderByDescending(a => a.Id)));
        }
        [View(Config.PurchaseOrders.OtherIn)]

        public ActionResult PreviewOtherOut(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateOtherOutReceipt", "PurchaseOrders");

        }


        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult AddOtherOut()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OTO";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult AddOtherOut(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "OTO";
            if (!ModelState.IsValid)
            {
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("OtherOut");
                po.TransCode = "OTO-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateOtherOutReceipt", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult AddOtherOutItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.OtherOut)]
        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult AddOtherOutItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult DeleteOtherOut(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The OtherOut deleted successfully", AlertType.Success);
                return RedirectToAction("OtherOutList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("OtherOutList");

        }

        [Manage(Config.PurchaseOrders.OtherOut)]

        public ActionResult GenerateOtherOutReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("OtherOutList");
            }
            return View(po);
        }

        //Expiry
        [View(Config.PurchaseOrders.Expiry)]

        public ActionResult ExpiryList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "EXP").OrderByDescending(a => a.Id)));
        }
        [View(Config.PurchaseOrders.Expiry)]

        public ActionResult PreviewExpiry(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
           
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateExpiryReceipt", "PurchaseOrders");

        }


        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult AddExpiry()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "EXP";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]

        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult AddExpiry(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "EXP";
            if (!ModelState.IsValid)
            {
                
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Expiry");
                po.TransCode = "EXP-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
               
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateExpiryReceipt", "PurchaseOrders");


        }
        [HttpGet]
        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult UpdateExpiry(int id)
        {


            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            TransMasterViewModel productVm = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))));
           
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();

            }
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }

            productVm.TransDetailViewModels = _unitOfWork.TransDetailRepository
                .GetTransDetails(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current))).ToList();

            foreach (var modifierVmModifierOptionViewModel in productVm.TransDetailViewModels)
            {
                PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductByCode(modifierVmModifierOptionViewModel.ProductCode, modifierVmModifierOptionViewModel.StoreId), modifierVmModifierOptionViewModel.Quantity, modifierVmModifierOptionViewModel.UnitPrice,userid);
            }

            productVm.TransDetailViewModels = PoHelper.temptTransDetail;
            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddExpiry", productVm);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult UpdateExpiry(int id, TransMasterViewModel purchasingVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
       

            try
            {

                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddPurchasing", purchasingVm);

                }
                else

                {
                    TransMaster transMaster = Mapper.Map<TransMaster>(purchasingVm);
                    transMaster.Type = "EXP";
                    IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                    transMaster.TotalPrice = (from a in poItems
                                              select a.Quantity * a.UnitPrice).Sum();
                    transMaster.TransDetails = new List<TransDetail>();
                    foreach (var productSubViewModel in poItems)
                    {
                        productSubViewModel.StoreId = transMaster.StoreId;
                        productSubViewModel.TransMasterId = transMaster.Id;
                        transMaster.TransDetails.Add(Mapper.Map<TransDetail>(productSubViewModel));

                    }
                    _unitOfWork.TransMasterRepository.UpdateExpiry(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)), transMaster);

                    _unitOfWork.Complete();
             


                    TempData["Alert"] = new AlertModel("The Expiry updated successfully", AlertType.Success);
                    return RedirectToAction("ExpiryList", "PurchaseOrders");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return View("AddExpiry", purchasingVm);



        }

        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult AddExpiryItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult AddExpiryItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult GenerateExpiryReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("ExpiryList");
            }
            return View(po);
        }
        [Manage(Config.PurchaseOrders.Expiry)]

        public ActionResult DeleteExpiry(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Expiry deleted successfully", AlertType.Success);
                return RedirectToAction("ExpiryList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("ExpiryList");

        }
        //Waste
        [View(Config.PurchaseOrders.Waste)]

        public ActionResult WasteList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "WST").OrderByDescending(a => a.Id)));
        }
        [View(Config.PurchaseOrders.Waste)]

        public ActionResult PreviewWaste(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateWasteReceipt", "PurchaseOrders");

        }


        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult AddWaste()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "WST";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult AddWaste(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "WST";
            if (!ModelState.IsValid)
            {
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Waste");
                po.TransCode = "WST-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateWasteReceipt", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult AddWasteItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult AddWasteItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult GenerateWasteReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("WasteList");
            }
            return View(po);
        }

        [Manage(Config.PurchaseOrders.Waste)]

        public ActionResult DeleteWaste(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Waste deleted successfully", AlertType.Success);
                return RedirectToAction("WasteList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("WasteList");

        }
        //Damage
        [View(Config.PurchaseOrders.Damage)]

        public ActionResult DamageList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.Type == "DMG").OrderByDescending(a => a.Id)));
        }
        [View(Config.PurchaseOrders.Damage)]

        public ActionResult PreviewDamage(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
           
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateDamageReceipt", "PurchaseOrders");

        }


        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult AddDamage()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "DMG";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult AddDamage(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "DMG";
            if (!ModelState.IsValid)
            {
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Damage");
                po.TransCode = "DMG-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateDamageReceipt", "PurchaseOrders");


        }
        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult AddDamageItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult AddDamageItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult DeleteDamage(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Damage deleted successfully", AlertType.Success);
                return RedirectToAction("DamageList", "PurchaseOrders");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
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
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("DamageList");

        }
        [Manage(Config.PurchaseOrders.Damage)]

        public ActionResult GenerateDamageReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("DamageList");
            }
            return View(po);
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

        public ActionResult GenerateReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel) TempData["po"];
            if (po == null)
            {
                return RedirectToAction("PurchaseOrderList");
            }
            return View(po);
        }

        public ActionResult GenerateStockReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("StockList");
            }
            return View(po);
        }

        public ActionResult GenerateTransferReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("TransferList");
            }
            return View(po);
        }
        public ActionResult GeneratePurchasingReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("PurchasingList");
            }
            return View(po);
        }
        public ActionResult GenerateOtherInReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("OtherInList");
            }
            return View(po);
        }
       
        public JsonResult GetPurchasingInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                PurchasingDdlViewModel purchase =
                    Mapper.Map<PurchasingDdlViewModel>(
                        _unitOfWork.TransMasterRepository.GetPurchaseById(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                return Json(purchase, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetTransactionsStockInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                var stock =
                    
                        _unitOfWork.ReportsRepository.GenerateTransactionsWarehouseStock(id);
                return Json(stock, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetExpiryInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                PurchasingDdlViewModel purchase =
                    Mapper.Map<PurchasingDdlViewModel>(
                        _unitOfWork.TransMasterRepository.GetPurchaseById(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                return Json(purchase, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetProductInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductDdlViewModel product =
                    Mapper.Map<ProductDdlViewModel>(
                        _unitOfWork.ProductRepository.GetProductById(id, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}