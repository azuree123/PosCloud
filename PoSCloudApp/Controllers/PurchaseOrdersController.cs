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
        public ActionResult PurchaseOrdersList()
        {
            return View(_unitOfWork.PurchaseOrderRepository.GetPurchaseOrders());
        }
        public ActionResult PurchaseOrderAdd()
        {

            return View();
        }
        public ActionResult PurchaseOrderUpdate()
        {
            return View();
        }
        public ActionResult PurchaseOrderDelete()
        {
            return View();
        }

        public ActionResult PurchaseOrdersDetailsList(int purchaseOrderId)
        {
            return View(_unitOfWork.PurchaseOrderDetailRepository.GetPurchaseOrderDetails(purchaseOrderId));
        }
        public ActionResult PurchaseOrderDetailsAdd()
        {

            return View();
        }
        public ActionResult PurchaseOrderDetailsUpdate()
        {
            return View();
        }
        public ActionResult PurchaseOrderDetailsDelete()
        {
            return View();
        }

    }
}