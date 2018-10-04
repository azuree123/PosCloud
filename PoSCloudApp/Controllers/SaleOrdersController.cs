using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;

namespace PoSCloudApp.Controllers
{
    public class SaleOrdersController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public SaleOrdersController()
        {

        }

        public SaleOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: SaleOrders
        public ActionResult SaleOrderList()
        {
            return View(_unitOfWork.SaleOrderRepository.GetSaleOrders());
        }

        public ActionResult SaleOrderAdd()
        {
            return View();
        }
        public ActionResult SaleOrderUpdate()
        {
            return View();
        }
        public ActionResult SaleOrderDelete()
        {
            return View();
        }

        public ActionResult SaleOrderDetailList(int saleOrderId)
        {
            return View(_unitOfWork.SaleOrderDetailRepository.GetSaleOrderDetails(saleOrderId));
        }
        public ActionResult SaleOrderDetailAdd()
        {
            return View();
        }
        public ActionResult SaleOrderDetailUpdate()
        {
            return View();
        }
        public ActionResult SaleOrderDetailDelete()
        {
            return View();
        }
    }
}