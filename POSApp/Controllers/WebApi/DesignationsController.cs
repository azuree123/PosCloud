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
    public class DesignationsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public DesignationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetDesignations(int storeId)
        {
            return Ok(Mapper.Map<DesignationViewModel[]>(await _unitOfWork.DesignationRepository.GetDesignationsAsync(storeId)));
        }

        // GET: api/DesignationsSync/5
        public async Task<IHttpActionResult> GetDesignation(int id, int storeId)
        {
            return Ok(await _unitOfWork.DesignationRepository.GetDesignationByIdAsync(id, storeId));
        }

        // POST: api/DesignationsSync
        public async Task<IHttpActionResult> AddDesignations([FromBody]SyncObject sync)
        {
            try
            {
                List<Designation> Designations = System.Web.Helpers.Json.Decode<List<Designation>>(sync.Object);
                foreach (var Designation in Designations)
                {
                    Designation.Code = Designation.Id.ToString();
                    Designation.Synced = true;
                    Designation.SyncedOn = DateTime.Now;
                    await _unitOfWork.DesignationRepository.AddDesignationAsync(Designation);
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

        // PUT: api/DesignationsSync/5
        public void UpdateDesignation(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DesignationsSync/5
        public void DeleteDesignation(int id)
        {
        }
    }
}