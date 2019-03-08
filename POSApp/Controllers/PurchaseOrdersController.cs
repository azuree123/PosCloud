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

namespace POSApp.Controllers
{
    [Authorize]
    public class PurchaseOrdersController : Controller
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
        public ActionResult PurchaseOrderList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "PRO")));
        }
        public ActionResult DailyPurchaseOrderList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMastersByDate((int)user.StoreId).Where(a => a.Type == "PRO")));
        }
        public ActionResult PreviewPurchaseOrder(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int) user.StoreId));
            temp.TransDetailViewModels = 
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int) user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
        return RedirectToAction("GenerateReceipt","PurchaseOrders");

    }

    public ActionResult PurchaseOrderDetailList(int purchaseOrderId)
    {
        return View();
    }
        
        public ActionResult AddPurchaseOrder()
        {
            TransMasterViewModel po=new TransMasterViewModel();
            po.Type = "PRO";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S",(int)user.StoreId).Select(a => new SelectListItem{Value = a.Id.ToString(),Text = a.Name});
            if (PoHelper.temptTransDetail != null)
            {
                
                PoHelper.EmptyTemptTransDetail(user.Id,(int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddPurchaseOrder(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "PRO";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {
               
                int TransId = _unitOfWork.AppCountersRepository.GetId("Purchase");
                po.TransCode = "PRO-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;
                
                var savePo = Mapper.Map<TransMaster>(po);
                
                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a=>a.CreatedByUserId==userid && a.StoreId==user.StoreId);

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
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int) user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
                return RedirectToAction("GenerateReceipt","PurchaseOrders");
            

        }

        public ActionResult AddTransactionItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a=>(a.PurchaseItem||a.InventoryItem)).Select(a=>new SelectListItem{Text = a.Name,Value = a.Id.ToString()}));
        }
        [HttpPost]
        public ActionResult AddTransactionItem(int productId,int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail=new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int) user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity,cost,user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        public ActionResult RemoveTransactionItem(string productId)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
                return View("PoTable", PoHelper.temptTransDetail);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            PoHelper.RemoveFromTemptTransDetail(productId, (int)user.StoreId, user.Id);
            return View("PoTable",PoHelper.temptTransDetail);
        }

        //Stock

        public ActionResult PreviewStock(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateStockReceipt", "PurchaseOrders");

        }
        public ActionResult StockList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "STI")));
        }
        public ActionResult AddStock()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "STI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddStock(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "STI";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("StockIn");
                po.TransCode = "STI-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("StockList", "PurchaseOrders");


        }

        public ActionResult AddStockItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddStockItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        //Transfer
        public ActionResult PreviewTransferOrder(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
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
        public ActionResult TransferList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "TRS")));
        }
        public ActionResult AddTransfer()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "TRS";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            po.FromStoreId = (int)user.StoreId;
            po.StoreId = (int) user.StoreId;


            var store = _unitOfWork.StoreRepository.GetStoreById((int) user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int) store.ClientId);

            po.FromStoreDdl = clientStores.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            po.ToStoreDdl = clientStores.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddTransfer(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "TRS";
            if (!ModelState.IsValid)
            {
                po.FromStoreDdl = _unitOfWork.StoreRepository.GetStores().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                po.ToStoreDdl = _unitOfWork.StoreRepository.GetStores().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Transfer");
                po.TransCode = "TRS-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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

        public ActionResult AddTransferItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddTransferItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }


        //Purchasing

        public ActionResult PreviewPurchasing(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GeneratePurchasingReceipt", "PurchaseOrders");

        }
        public ActionResult PurchasingList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "PRI")));
        }
        public ActionResult AddPurchasing()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "PRI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int) user.StoreId)
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.TransCode});
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddPurchasing(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "PRI";
            if (!ModelState.IsValid)
            {
                po.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.TransCode });
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Purchasing");
                po.TransCode = "PRI-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateReceipt", "PurchaseOrders");


        }

        public ActionResult AddPurchasingItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => (a.PurchaseItem || a.InventoryItem)).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddPurchasingItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }
        public ActionResult UpdatePurchasing(int id)
        {

            
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
           
            TransMasterViewModel productVm = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(user.StoreId)));
            productVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();

            }
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }

            productVm.TransDetailViewModels = _unitOfWork.TransDetailRepository
                .GetTransDetails(id, Convert.ToInt32(user.StoreId)).ToList();
            foreach (var modifierVmModifierOptionViewModel in productVm.TransDetailViewModels)
            {
                PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductByCode(modifierVmModifierOptionViewModel.ProductCode,modifierVmModifierOptionViewModel.StoreId),modifierVmModifierOptionViewModel.Quantity,modifierVmModifierOptionViewModel.UnitPrice,userid);
            }

            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddPurchasing", productVm);
        }
        [HttpPost]
        public ActionResult UpdatePurchasing(int id, TransMasterViewModel purchasingVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            purchasingVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

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
                    
                    _unitOfWork.TransMasterRepository.UpdateTransMaster(id, Convert.ToInt32(user.StoreId),transMaster);
                    _unitOfWork.Complete();
                    List<TransDetailViewModel> products = _unitOfWork.TransDetailRepository.GetTransDetails(transMaster.Id,transMaster.StoreId).ToList();
                    foreach (var productsSub in products)
                    {
                        _unitOfWork.TransDetailRepository.DeleteTransDetail(productsSub.Id, productsSub.StoreId);
                    }
                    foreach (var productSubViewModel in PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid))
                    {
                        productSubViewModel.StoreId = transMaster.StoreId;
                        productSubViewModel.Id = transMaster.Id;
                        _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(productSubViewModel));

                    }
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
        //End Purchasing
        public ActionResult GetPurchaseOrder(int id)
        {

            TransMasterViewModel productVm = new TransMasterViewModel();
            productVm.Type = "PRI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TransMasterViewModel temp = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, Convert.ToInt32(user.StoreId)));
            productVm.BusinessPartnerId = temp.BusinessPartnerId;
            productVm.PriDdl = _unitOfWork.TransMasterRepository.GetPurchaseInvoices((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.TransCode });
            productVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();

            }
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }

            productVm.TransDetailViewModels = _unitOfWork.TransDetailRepository
                .GetTransDetails(id, Convert.ToInt32(user.StoreId)).ToList();
            foreach (var modifierVmModifierOptionViewModel in productVm.TransDetailViewModels)
            {
                PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductByCode(modifierVmModifierOptionViewModel.ProductCode, modifierVmModifierOptionViewModel.StoreId), modifierVmModifierOptionViewModel.Quantity, modifierVmModifierOptionViewModel.UnitPrice, userid);
            }

            ViewBag.js = "<script>ChangeTableFill();</script>";
            return View("AddPurchasing", productVm);
        }
        [HttpPost]
        public ActionResult GetPurchaseOrder(int id, TransMasterViewModel purchasingVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            purchasingVm.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });

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
                    purchasingVm.TransCode = "PRI-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                    purchasingVm.StoreId = user.StoreId;

                    var savePo = Mapper.Map<TransMaster>(purchasingVm);

                    IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                    temp.TransMasterViewModel = purchasingVm;
                    temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                    temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
                    temp.BusinessPartnerViewModel =
                        Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)purchasingVm.BusinessPartnerId, (int)user.StoreId));
                    temp.TransDetailViewModels = poItems;
                    temp.TotalAmount = (from a in temp.TransDetailViewModels
                        select a.Quantity * a.UnitPrice).Sum();
                    TempData["po"] = temp;
                }
                return RedirectToAction("GenerateReceipt", "PurchaseOrders");

            
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

        public ActionResult OtherInList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "OTI")));
        }
       
        public ActionResult PreviewOtherIn(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateOtherInReceipt", "PurchaseOrders");

        }

       

        public ActionResult AddOtherIn()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OTI";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddOtherIn(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "OTI";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("OtherIn");
                po.TransCode = "OTI-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateOtherInReceipt", "PurchaseOrders");


        }

        public ActionResult AddOtherInItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddOtherInItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }


        //Other Out

        public ActionResult OtherOutList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "OTO")));
        }

        public ActionResult PreviewOtherOut(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateOtherOutReceipt", "PurchaseOrders");

        }



        public ActionResult AddOtherOut()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OTO";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddOtherOut(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "OTO";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("OtherOut");
                po.TransCode = "OTO-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateOtherOutReceipt", "PurchaseOrders");


        }

        public ActionResult AddOtherOutItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddOtherOutItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

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

        public ActionResult ExpiryList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "EXP")));
        }

        public ActionResult PreviewExpiry(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateExpiryReceipt", "PurchaseOrders");

        }



        public ActionResult AddExpiry()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "EXP";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddExpiry(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "EXP";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Expiry");
                po.TransCode = "EXP-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateExpiryReceipt", "PurchaseOrders");


        }

        public ActionResult AddExpiryItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddExpiryItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        public ActionResult GenerateExpiryReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("ExpiryList");
            }
            return View(po);
        }

        //Waste

        public ActionResult WasteList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "WST")));
        }

        public ActionResult PreviewWaste(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateWasteReceipt", "PurchaseOrders");

        }



        public ActionResult AddWaste()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "WST";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddWaste(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "WST";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Waste");
                po.TransCode = "WST-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateWasteReceipt", "PurchaseOrders");


        }

        public ActionResult AddWasteItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddWasteItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

        public ActionResult GenerateWasteReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("WasteList");
            }
            return View(po);
        }

        //Damage

        public ActionResult DamageList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "DMG")));
        }

        public ActionResult PreviewDamage(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
            temp.BusinessPartnerViewModel =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateDamageReceipt", "PurchaseOrders");

        }



        public ActionResult AddDamage()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "DMG";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (PoHelper.temptTransDetail != null)
            {

                PoHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddDamage(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "DMG";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("Damage");
                po.TransCode = "DMG-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner((int)po.BusinessPartnerId, (int)user.StoreId));
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateDamageReceipt", "PurchaseOrders");


        }

        public ActionResult AddDamageItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => a.InventoryItem).Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddDamageItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            PoHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", PoHelper.temptTransDetail);
        }

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
                        _unitOfWork.TransMasterRepository.GetPurchaseById(id, (int)user.StoreId));
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
                        _unitOfWork.ProductRepository.GetProductById(id, (int) user.StoreId));
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