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
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).Where(a => a.Type == "PRI")));
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
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner(temp.TransMasterViewModel.BusinessPartnerId, (int)user.StoreId));
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
            po.Type = "PRI";
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
            po.Type = "PRI";
            if (!ModelState.IsValid)
            {
                po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {
               
                int TransId = _unitOfWork.AppCountersRepository.GetId("Invoice");
                po.TransCode = "INV-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;
                var savePo = Mapper.Map<TransMaster>(po);
               
                IEnumerable<TransDetailViewModel> poItems = PoHelper.temptTransDetail.Where(a=>a.CreatedByUserId==userid && a.StoreId==user.StoreId);

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
                temp.BusinessPartnerViewModel =
                    Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner(po.BusinessPartnerId, (int) user.StoreId));
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
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Select(a=>new SelectListItem{Text = a.Name,Value = a.Id.ToString()}));
        }
        [HttpPost]
        public ActionResult AddTransactionItem(int productId,int quantity,decimal cost)
        {
            if (PoHelper.temptTransDetail == null)
            {
                PoHelper.temptTransDetail=new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            PoHelper.AddToTemptTransDetail(_unitOfWork.ProductRepository.GetProductById(productId,(int)user.StoreId),quantity,cost,user.Id);
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