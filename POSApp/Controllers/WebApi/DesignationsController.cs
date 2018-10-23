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
        public async Task<IHttpActionResult> GetDesignations()
        {
            return Ok(Mapper.Map<DesignationViewModel[]>(_unitOfWork.DesignationRepository.GetApiDesignations()));
        }

        // GET: api/DesignationsSync/5
        public async Task<IHttpActionResult> GetDesignation(int id, int storeId)
        {
            return Ok(_unitOfWork.DesignationRepository.GetDesignationById(id));
        }

        // POST: api/DesignationsSync
        public async Task<IHttpActionResult> AddDesignations([FromBody]SyncObject sync)
        {
            try
            {
                List<Designation> designations = System.Web.Helpers.Json.Decode<List<Designation>>(sync.Object);
                foreach (var designation in designations)
                {
                    designation.Code = designation.Id.ToString();
                    designation.Synced = true;
                    designation.SyncedOn = DateTime.Now;
                    _unitOfWork.DesignationRepository.AddDesignation(designation);
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