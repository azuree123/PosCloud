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
    public class DevicesController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public DevicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Devices
        public async Task<IHttpActionResult> GetDevices(int storeId)
        {
            return Ok(Mapper.Map<DeviceViewModel[]>(_unitOfWork.DeviceRepository.GetDevices(storeId)));
        }

        // GET: api/Devices/5
        public async Task<IHttpActionResult> GetDevice(int id, int storeId)
        {
            return Ok(_unitOfWork.DeviceRepository.GetDeviceById(id, storeId));
        }

        // POST: api/Devices
        public async Task<IHttpActionResult> AddDevice([FromBody]SyncObject sync)
        {
            try
            {
                List<Device> devices = System.Web.Helpers.Json.Decode<List<Device>>(sync.Object);
                foreach (var device in devices)
                {
                    device.Code = device.Id.ToString();
                    device.Synced = true;
                    device.SyncedOn = DateTime.Now;
                    _unitOfWork.DeviceRepository.AddDevice(device);
                }
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Devices/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Devices/5
        public void Delete(int id)
        {
        }
    }
}
