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
    public class TimedEventsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public TimedEventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/TimedEvents
        public async Task<IHttpActionResult> GetTimedEvents(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.TimedEventRepository.GetTimedEventsAsync(storeId);


                return Ok(Mapper.Map<TimedEventViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "TimedEvents");
                if (lastSync == null)
                {
                    data = await _unitOfWork.TimedEventRepository.GetTimedEventsAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.TimedEventRepository.GetAllTimedEventsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "TimedEvents"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<TimedEventViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetTimedEvents(int storeId)
        //{
        //    return Ok(Mapper.Map<TimedEventViewModel[]>(await _unitOfWork.TimedEventRepository.GetTimedEventsAsync(storeId)));
        //}

        // GET: api/TimedEvents/5
        public async Task<IHttpActionResult> GetTimedEvent(int id, int storeId)
        {
            return Ok(await _unitOfWork.TimedEventRepository.GetTimedEventByIdAsync(id, storeId));
        }

        // POST: api/TimedEvents
        public async Task<IHttpActionResult> AddTimedEvent([FromBody]SyncObject sync)
        {
            try
            {
                List<TimedEvent> timedEvents = System.Web.Helpers.Json.Decode<List<TimedEvent>>(sync.Object);
                foreach (var timedEvent in timedEvents)
                {
                    timedEvent.Code = timedEvent.Id.ToString();
                    timedEvent.Synced = true;
                    timedEvent.SyncedOn = DateTime.Now;
                    await _unitOfWork.TimedEventRepository.AddTimedEventAsync(timedEvent);
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

        // PUT: api/TimedEvents/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TimedEvents/5
        public void Delete(int id)
        {
        }
    }
}
