using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using POSApp.Core;

namespace POSApp.Controllers.WebApi
{
    public class AppCountersController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public AppCountersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IHttpActionResult> GetAppCounter(int storeId, int deviceId)
        {
            return Ok(await _unitOfWork.AppCountersRepository.GetAppCounterAsync(storeId, deviceId));
        }
    }
}
