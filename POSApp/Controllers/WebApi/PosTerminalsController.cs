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
    public class PosTerminalsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public PosTerminalsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/TillOperations
        public async Task<IHttpActionResult> GetPosTerminals(int storeId)
        {
            return Ok(Mapper.Map<POSTerminalViewModel[]>(_unitOfWork.TillOperationRepository.GetTillOperations(storeId)));
        }

        // GET: api/TillOperations/5
        public async Task<IHttpActionResult> GetPosTerminal(int id, int storeId)
        {
            return Ok(_unitOfWork.POSTerminalRepository.GetPOSTerminalById(id, storeId));
        }

        // POST: api/TillOperations
        public async Task<IHttpActionResult> AddPosTerminal([FromBody]SyncObject sync)
        {
            try
            {
                List<POSTerminal> posTerminals = System.Web.Helpers.Json.Decode<List<POSTerminal>>(sync.Object);
                foreach (var posTerminal in posTerminals)
                {
                    posTerminal.Code = posTerminal.POSTerminalId.ToString();
                    posTerminal.Synced = true;
                    posTerminal.SyncedOn = DateTime.Now;
                    _unitOfWork.POSTerminalRepository.AddPOSTerminal(posTerminal);
                }
                _unitOfWork.Complete();
                return Ok("Success");
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
