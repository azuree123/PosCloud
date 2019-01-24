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
        public async Task<IHttpActionResult> GetSuppliers(int storeId)
        {
            return Ok(Mapper.Map<SupplierModelView[]>(await _unitOfWork.BusinessPartnerRepository.GetBusinessPartnersAsync("S", storeId)));
        }

        // GET: api/SuppliersSync/5
        public async Task<IHttpActionResult> GetSupplier(int id,int storeId)
        {
            return Ok(await _unitOfWork.BusinessPartnerRepository.GetBusinessPartnerAsync(id,storeId));
        }

        // POST: api/SuppliersSync
        public async Task<IHttpActionResult> AddSuppliers([FromBody]SyncObject sync)
        {
            try
            {
                List<BusinessPartner> suppliers = System.Web.Helpers.Json.Decode<List<BusinessPartner>>(sync.Object);
                foreach (var supplier in suppliers)
                {
                    supplier.Code = supplier.Id.ToString();
                    supplier.Type = "S";
                    supplier.Synced = true;
                    supplier.SyncedOn = DateTime.Now;
                    await _unitOfWork.BusinessPartnerRepository.AddBusinessPartnerAsync(supplier);
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
