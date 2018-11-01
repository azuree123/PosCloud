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
            return null;
        }

        // POST: api/Sync
        public async Task<IHttpActionResult> AddSaleOrder([FromBody]SyncObject sync)
       {
            try
            {

                SalesViewModel salesView = System.Web.Helpers.Json.Decode<SalesViewModel>(sync.Object) ;
                TransMaster saleOrder = salesView.SaleOrder;
                saleOrder.Code = saleOrder.Id.ToString();
                var saleOrderAdd = saleOrder;
                _unitOfWork.TransMasterRepository.AddTransMaster(saleOrder);
                foreach (var saleOrderDetail in salesView.SaleOrderDetails)
                {
                    saleOrderDetail.Code = salesView.SaleOrder.Id.ToString();
                    saleOrderDetail.TransMasterId = saleOrderAdd.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(saleOrderDetail);
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
