using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class SalesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sync
        public SalesViewModel Get()
        {
            return null;
        }

        // GET: api/Sync/5
        public SalesViewModel Get(int id)
        {
            SalesViewModel model=new SalesViewModel();
            model.SaleOrder=new SaleOrder
            {
                Amount = 0,Canceled = false,Code ="0001",CustomerId = 1,Date = "10/22/2018",Discount = 0,Status = "HHH",
                Tax = 0,Time = "16:00:00",Type = "Product"
            };
            model.SaleOrderDetails=new List<SaleOrderDetail>();
            model.SaleOrderDetails.Add(new SaleOrderDetail{Discount = 0,ProductId = 18355,Quantity = 12,UnitPrice = 20});
            return model;
        }

        // POST: api/Sync
        public async Task<IHttpActionResult> AddSaleOrder([FromBody]SyncObject sync)
       {
            try
            {

                SalesViewModel salesView = System.Web.Helpers.Json.Decode<SalesViewModel>(sync.Object) ;
                SaleOrder saleOrder = salesView.SaleOrder;
                saleOrder.Code = saleOrder.Id.ToString();
                var saleOrderAdd = saleOrder;
                _unitOfWork.SaleOrderRepository.AddSaleOrder(saleOrder);
                foreach (var saleOrderDetail in salesView.SaleOrderDetails)
                {
                    saleOrderDetail.Code = salesView.SaleOrder.Id.ToString();
                    saleOrderDetail.SaleOrderId = saleOrderAdd.Id;
                    _unitOfWork.SaleOrderDetailRepository.AddSaleOrderDetail(saleOrderDetail);
                }
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                throw;
            }
        }

        // PUT: api/Sync/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sync/5
        public void Delete(int id)
        {
        }
    }


}
