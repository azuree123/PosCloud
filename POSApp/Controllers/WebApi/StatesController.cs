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

namespace POSApp.Controllers.WebApi
{
    public class StatesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public StatesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetStates()
        {
            return Ok(Mapper.Map<StateModelView[]>(await _unitOfWork.StateRepository.GetStatesAsync()));
        }

        // GET: api/StateCategoriesSync/5
        public async Task<IHttpActionResult> GetState(int id, int storeId)
        {
            return Ok(await _unitOfWork.StateRepository.GetStateByIdAsync(id));
        }

        // POST: api/StateCategoriesSync
        public async Task<IHttpActionResult> AddStates([FromBody]SyncObject sync)
        {
            try
            {
                List<State> states = System.Web.Helpers.Json.Decode<List<State>>(sync.Object);
                foreach (var state in states)
                {
                    state.Code = state.Id.ToString();
                    state.Synced = true;
                    state.SyncedOn = DateTime.Now;
                    await _unitOfWork.StateRepository.AddStateAsync(state);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok("Error");
                throw;
            }
        }

        // PUT: api/StateCategoriesSync/5
        public void UpdateState(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StateCategoriesSync/5
        public void DeleteState(int id)
        {
        }
    }
}
