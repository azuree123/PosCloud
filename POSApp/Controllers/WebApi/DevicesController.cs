using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSApp.Core;

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
        public IEnumerable<string> GetDevices(int storeId)
        {
            //return _unitOfWork.;
        }

        // GET: api/Devices/5
        public string GetDevice(int id)
        {
            return "value";
        }

        // POST: api/Devices
        public void AddDevice([FromBody]string value)
        {
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
