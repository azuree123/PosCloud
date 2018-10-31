using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
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
            return View(_unitOfWork.PurchaseOrderRepository.GetPurchaseOrders());
        }
       
       

        public ActionResult PurchaseOrderDetailList(int purchaseOrderId)
        {
            return View(_unitOfWork.PurchaseOrderDetailRepository.GetPurchaseOrderDetails(purchaseOrderId));
        }
        
        public ActionResult AddPurchaseOrder()
        {
            TransMasterViewModel po=new TransMasterViewModel();
            po.Type = "PRI";
            po.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers().Select(a => new SelectListItem{Value = a.Id.ToString(),Text = a.Name});
            if (PoHelper.temptTransDetail != null)
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                PoHelper.EmptyTemptTransDetail(user.Id,(int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddPurchaseOrder(TransMasterViewModel po)
        {
            po.Type = "PRI";
            if (!ModelState.IsValid)
            {
            po.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers().Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                return View(po);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int TransId = _unitOfWork.AppCountersRepository.GetId("Invoice");
                po.TransCode = "INV-" + "C-" + TransId.ToString() + "-" + user.StoreId;

            }
            

            return View();
        }

        public ActionResult AddTransactionItem()
        {
            return View(_unitOfWork.ProductRepository.GetAllProducts().Select(a=>new SelectListItem{Text = a.Name,Value = a.Id.ToString()}));
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
        public ActionResult RemoveTransactionItem(int productId)
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

    }
}