﻿using System;
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
        [HttpGet]
        //public ActionResult AddPurchaseOrder()
        //{
        //    ViewBag.edit = "AddPurchaseOrder";
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddPurchaseOrder()
        //{

        //}
       

        public ActionResult PurchaseOrderDetailList(int purchaseOrderId)
        {
            return View(_unitOfWork.PurchaseOrderDetailRepository.GetPurchaseOrderDetails(purchaseOrderId));
        }
        
    }
}