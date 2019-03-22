using System;
using System.Collections.Generic;
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
    public class StockTakingController : Controller
    {
        // GET: StockTaking
        private IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;
        public StockTakingController()
        {

        }

        public StockTakingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PurchaseOrders
        public ActionResult StockTakingList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).OrderBy(a => a.TransDate).Where(a => a.Type == "STI")));
        }

        public ActionResult PreviewStockTaking(int id)
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
            return RedirectToAction("GenerateStockTakingReceipt", "StockTaking");

        }



        public ActionResult AddStockTaking()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OPS";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            if (TakingHelper.temptTransDetail != null)
            {

                TakingHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddStockTaking(TransMasterViewModel po)
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

                int TransId = _unitOfWork.AppCountersRepository.GetId("StockTaking");
                po.TransCode = "STI-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = TakingHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

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
            return RedirectToAction("GenerateStockTakingReceipt", "StockTaking");


        }

        public ActionResult AddTransactionItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => (a.PurchaseItem || a.InventoryItem)).OrderBy(a => a.Name).Select(a => new SelectListItem { Text = a.Name + "(" + a.ProductCode + ")", Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddTransactionItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (TakingHelper.temptTransDetail == null)
            {
                TakingHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            TakingHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", TakingHelper.temptTransDetail);
        }
        public ActionResult RemoveTransactionItem(string productId)
        {
            if (TakingHelper.temptTransDetail == null)
            {
                TakingHelper.temptTransDetail = new List<TransDetailViewModel>();
                return View("PoTable", TakingHelper.temptTransDetail);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TakingHelper.RemoveFromTemptTransDetail(productId, (int)user.StoreId, user.Id);
            return View("PoTable", TakingHelper.temptTransDetail);
        }

        public ActionResult GenerateStockTakingReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("StockTakingList");
            }
            return View(po);
        }
        public JsonResult GetProductInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductDdlViewModel product =
                    Mapper.Map<ProductDdlViewModel>(
                        _unitOfWork.ProductRepository.GetProductById(id, (int)user.StoreId));
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
