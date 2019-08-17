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
    public class UnitsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public UnitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Units

        public async Task<IHttpActionResult> GetUnits(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.UnitRepository.GetUnitAsync(storeId);


                return Ok(Mapper.Map<UnitViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Units");
                if (lastSync == null)
                {
                    data = await _unitOfWork.UnitRepository.GetUnitAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.UnitRepository.GetAllUnitsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Units"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<UnitViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetUnits(int storeId)
        //{
        //    return Ok(Mapper.Map<UnitViewModel[]>(await _unitOfWork.UnitRepository.GetUnitAsync(storeId)));
        //}

        // GET: api/UnitCategoriesSync/5
        public async Task<IHttpActionResult> GetUnit(int id, int storeId)
        {
            return Ok(await _unitOfWork.UnitRepository.GetUnitByIdAsync(id, storeId));
        }

        // POST: api/UnitCategoriesSync
        public async Task<IHttpActionResult> AddUnits([FromBody]SyncObject sync)
        {
            try
            {
                List<Unit> unites = System.Web.Helpers.Json.Decode<List<Unit>>(sync.Object);
                foreach (var unit in unites)
                {
                    unit.Code = unit.Id.ToString();
                    unit.Synced = true;
                    unit.SyncedOn = DateTime.Now;
                    await _unitOfWork.UnitRepository.AddUnitAsync(unit);
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

        // PUT: api/UnitCategoriesSync/5
        public void UpdateUnit(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UnitCategoriesSync/5
        public void DeleteUnit(int id)
        {
        }
    }
}
