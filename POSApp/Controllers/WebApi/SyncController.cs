using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using POSApp.Core.Models;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class SyncController : ApiController
    {
        private SyncViewModel syncView = new SyncViewModel();
        // GET: api/Sync
        public SyncViewModel Get()
        {
            return syncView;
        }

        // GET: api/Sync/5
        public SyncViewModel Get(int id)
        {
            SyncViewModel model=new SyncViewModel();
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
        public async Task<IHttpActionResult> Post([FromBody]SyncViewModel sync)
       {
            try
            {
            syncView = sync;
                return Ok(syncView);
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
