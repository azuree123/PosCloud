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

        public async Task<IHttpActionResult> GetFloors(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.FloorRepository.GetFloorsAsync(storeId);


                return Ok(Mapper.Map<FloorViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Floors");
                if (lastSync == null)
                {
                    data = await _unitOfWork.FloorRepository.GetFloorsAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.FloorRepository.GetAllFloorsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Floors"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<FloorViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetFloors(int storeId)
        //{
        //    return Ok(Mapper.Map<FloorViewModel[]>(await _unitOfWork.FloorRepository.GetFloorsAsync(storeId)));
        //}

        // GET: api/FloorApi/5
        public async Task<IHttpActionResult> GetFloor(int id, int storeId)
        {
            return Ok(await _unitOfWork.FloorRepository.GetFloorByIdAsync(id, storeId));
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
                    await _unitOfWork.FloorRepository.AddFloorAsync(floor);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
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
