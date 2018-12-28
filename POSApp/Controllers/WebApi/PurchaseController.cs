using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.ViewModels;
using POSApp.Persistence;

namespace POSApp.Controllers.WebApi
{
    public class PurchaseController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public PurchaseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Purchase
        public async Task<IHttpActionResult> GetPurchases(int storeId)
        {
            return Ok(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters(storeId)));
        }

        // GET: api/Purchase/5
        public async Task<IHttpActionResult> GetPurchase(int id, int storeId)
        {
            return Ok(_unitOfWork.TransMasterRepository.GetTransMaster(id, storeId));
        }

        // POST: api/Purchase
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Purchase/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Purchase/5
        public void Delete(int id)
        {
        }
    }
}
