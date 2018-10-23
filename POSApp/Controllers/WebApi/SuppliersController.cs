using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels.Sync;
using POSApp.Persistence;

namespace POSApp.Controllers.WebApi
{
    public class SuppliersController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public SuppliersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/SuppliersSync
        public async Task<IHttpActionResult> GetSuppliers()
        {
            return Ok(_unitOfWork.SupplierRepository.GetApiSuppliers());
        }

        // GET: api/SuppliersSync/5
        public async Task<IHttpActionResult> GetSupplier(int id,int storeId)
        {
            return Ok(_unitOfWork.SupplierRepository.GetSupplierById(id,storeId));
        }

        // POST: api/SuppliersSync
        public async Task<IHttpActionResult> AddSuppliers([FromBody]SyncObject sync)
        {
            try
            {
                List<Supplier> suppliers = System.Web.Helpers.Json.Decode<List<Supplier>>(sync.Object);
                foreach (var supplier in suppliers)
                {
                    supplier.Code = supplier.Id.ToString();
                    _unitOfWork.SupplierRepository.AddSupplier(supplier);
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

        // PUT: api/SuppliersSync/5
        public void UpdateSupplier(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SuppliersSync/5
        public void DeleteSupplier(int id)
        {
        }

    }
}
