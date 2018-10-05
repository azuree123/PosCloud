using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;

namespace PoSCloudApp.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private IUnitOfWork _unitOfWork;

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
        public ActionResult AddPurchaseOrder()
        {

            return View();
        }
        public ActionResult UpdatePurchaseOrder()
        {
            return View();
        }
        public ActionResult DeletePurchaseOrder(int id)
        {
            _unitOfWork.PurchaseOrderRepository.DeletePurchaseOrder(id);
            return RedirectToAction("PurchaseOrderList", "PurchaseOrders");
        }

        public ActionResult PurchaseOrderDetailList(int purchaseOrderId)
        {
            return View(_unitOfWork.PurchaseOrderDetailRepository.GetPurchaseOrderDetails(purchaseOrderId));
        }
        public ActionResult AddPurchaseOrderDetail()
        {

            return View();
        }
        public ActionResult UpdatePurchaseOrderDetails()
        {
            return View();
        }
        public ActionResult DeletePurchaseOrderDetails(int id)
        {
            _unitOfWork.PurchaseOrderDetailRepository.DeletePurchaseOrderDetail(id);
            _unitOfWork.Complete();
            return RedirectToAction("PurchaseOrderDetailList","PurchaseOrders");
        }

    }
}