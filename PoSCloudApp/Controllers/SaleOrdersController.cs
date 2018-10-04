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

        public ActionResult AddSaleOrder()
        {
            return View();
        }
        public ActionResult UpdateSaleOrder()
        {
            return View();
        }
        public ActionResult DeleteSaleOrder(int id)
        {
            _unitOfWork.SaleOrderRepository.DeleteSaleOrder(id);
            _unitOfWork.Complete();
            return RedirectToAction("SaleOrderList","SaleOrders");
        }

        public ActionResult SaleOrderDetailList(int saleOrderId)
        {
            return View(_unitOfWork.SaleOrderDetailRepository.GetSaleOrderDetails(saleOrderId));
        }
        public ActionResult AddSaleOrderDetail()
        {
            return View();
        }
        public ActionResult UpdateSaleOrderDetail()
        {
            return View();
        }
        public ActionResult DeleteSaleOrderDetail(int id)
        {
            _unitOfWork.SaleOrderDetailRepository.DeleteSaleOrderDetail(id);
            _unitOfWork.Complete();
            return RedirectToAction("SaleOrderDetailList","SaleOrders");
        }
    }
}