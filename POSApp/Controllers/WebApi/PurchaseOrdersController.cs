using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class PurchaseOrdersController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public PurchaseOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetPurchaseOrders(int storeId)
        {
            return Ok(_unitOfWork.PurchaseOrderRepository.GetPurchaseOrders(storeId));
        }

        // GET: api/PurchaseOrderCategoriesSync/5
        public async Task<IHttpActionResult> GetPurchaseOrder(int id, int storeId)
        {
            return Ok(_unitOfWork.PurchaseOrderRepository.GetPurchaseOrderById(id));
        }

        // POST: api/PurchaseOrderCategoriesSync
        public async Task<IHttpActionResult> AddPurchaseOrders([FromBody]SyncObject sync)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = System.Web.Helpers.Json.Decode<List<PurchaseOrder>>(sync.Object);
                foreach (var purchaseOrder in purchaseOrders)
                {
                    purchaseOrder.Code = purchaseOrder.Id.ToString();
                    _unitOfWork.PurchaseOrderRepository.AddPurchaseOrder(purchaseOrder);
                }
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok("Error");
                throw;
            }
        }

        // PUT: api/PurchaseOrderCategoriesSync/5
        public void UpdatePurchaseOrder(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PurchaseOrderCategoriesSync/5
        public void DeletePurchaseOrder(int id)
        {
        }
    }
}
