using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;
using POSApp.Persistence;

namespace POSApp.Controllers.WebApi
{
    public class TillOperationsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public TillOperationsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/TillOperations
        public async Task<IHttpActionResult> GetTillOperations(int storeId)
        {
            return Ok(Mapper.Map<TillOperationViewModel[]>(_unitOfWork.TillOperationRepository.GetTillOperations(storeId)));
        }

        // GET: api/TillOperations/5
        public async Task<IHttpActionResult> GetTillOperation(int id ,int storeId)
        {
            return Ok(_unitOfWork.TillOperationRepository.GetTillOperationsById(id,storeId));
        }

        // POST: api/TillOperations
        public async Task<IHttpActionResult> AddTillOperation([FromBody]SyncObject sync)
        {
            try
            {
                List<TillOperation> tillOperations = System.Web.Helpers.Json.Decode<List<TillOperation>>(sync.Object);
                foreach (var tillOperation in tillOperations)
                {
                    tillOperation.Code = tillOperation.Id.ToString();
                    tillOperation.Synced = true;
                    tillOperation.SyncedOn = DateTime.Now;
                    _unitOfWork.TillOperationRepository.AddTillOperation(tillOperation);
                }
                _unitOfWork.Complete();
                return Ok(1);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/TillOperations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TillOperations/5
        public void Delete(int id)
        {
        }
    }
}
