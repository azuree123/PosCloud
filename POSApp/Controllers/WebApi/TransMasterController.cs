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
    public class TransMasterController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public TransMasterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetLastTransaction(string storeId)
        {
            return Ok(await _unitOfWork.TransMasterRepository.GetLastTransactionINV(storeId));
        }


    }
}
