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
    public class DineTablesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public DineTablesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/DineTables

        public async Task<IHttpActionResult> GetDineTables(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.DineTableRepository.GetDineTablesAsync(storeId);


                return Ok(Mapper.Map<DineTableViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "DineTables");
                if (lastSync == null)
                {
                    data = await _unitOfWork.DineTableRepository.GetDineTablesAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.DineTableRepository.GetAllTablesAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "DineTables"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<DineTableViewModel[]>(data));


            }


        }

        public async Task<IHttpActionResult> GetDineTables(int storeId)
        {
            return Ok(Mapper.Map<DineTableViewModel[]>(await _unitOfWork.DineTableRepository.GetDineTablesAsync(storeId)));
        }

        // GET: api/DineTables/5
        public async Task<IHttpActionResult> GetDineTable(int id, int storeId)
        {
            return Ok(await _unitOfWork.DineTableRepository.GetDineTableByIdAsync(id, storeId));
        }

        // POST: api/DineTables
        public async Task<IHttpActionResult> AddDineTable([FromBody]SyncObject sync)
        {
            try
            {
                List<DineTable> dineTables = System.Web.Helpers.Json.Decode<List<DineTable>>(sync.Object);
                foreach (var dineTable in dineTables)
                {
                    dineTable.Code = dineTable.Id.ToString();
                    dineTable.Synced = true;
                    dineTable.SyncedOn = DateTime.Now;
                    await _unitOfWork.DineTableRepository.AddDineTableAsync(dineTable);
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

        // PUT: api/DineTables/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DineTables/5
        public void Delete(int id)
        {
        }
    }
}
