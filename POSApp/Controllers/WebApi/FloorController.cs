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
    public class FloorController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public FloorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/FloorApi
        public async Task<IHttpActionResult> GetFloors(int storeId)
        {
            return Ok(Mapper.Map<FloorViewModel[]>(_unitOfWork.FloorRepository.GetFloors(storeId)));
        }

        // GET: api/FloorApi/5
        public async Task<IHttpActionResult> GetFloor(int id, int storeId)
        {
            return Ok(_unitOfWork.FloorRepository.GetFloorById(id, storeId));
        }

        // POST: api/FloorApi
        public async Task<IHttpActionResult> AddFloor([FromBody]SyncObject sync)
        {
            try
            {
                List<Floor> floors = System.Web.Helpers.Json.Decode<List<Floor>>(sync.Object);
                foreach (var floor in floors)
                {
                    floor.Code = floor.Id.ToString();
                    floor.Synced = true;
                    floor.SyncedOn = DateTime.Now;
                    _unitOfWork.FloorRepository.AddFloor(floor);
                }
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/FloorApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FloorApi/5
        public void Delete(int id)
        {
        }
    }
}
