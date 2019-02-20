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
            return _unitOfWork.TransMasterRepository.GetInvoiceById(4,3);
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

                List<SalesViewModel> salesView = System.Web.Helpers.Json.Decode<List<SalesViewModel>>(sync.Object) ;
                foreach (var salesViewModel in salesView)
                {

                    TransMaster saleOrder = salesViewModel.TransMaster;
                    saleOrder.Code = saleOrder.Id.ToString();
                    var saleOrderAdd = saleOrder;
                    await _unitOfWork.TransMasterRepository.AddTransMasterAsync(saleOrder);
                    
                  
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(0);
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
