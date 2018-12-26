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
    public class CustomersController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetCustomers(int storeId)
        {
            return Ok(Mapper.Map<CustomerModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("C", storeId)));
        }

        // GET: api/CustomersSync/5
        public async Task<IHttpActionResult> GetCustomer(int id, int storeId)
        {
            return Ok(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner(id,storeId));
        }

        // POST: api/CustomersSync
        public async Task<IHttpActionResult> AddCustomers([FromBody]SyncObject sync)
        {
            try
            {
                List<BusinessPartner> customers = System.Web.Helpers.Json.Decode<List<BusinessPartner>>(sync.Object);
                foreach (var customer in customers)
                {
                    customer.Code = customer.Id.ToString();
                    customer.Type = "C";
                    customer.Synced = true;
                    customer.SyncedOn = DateTime.Now;
                    _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(customer);
                }
                _unitOfWork.Complete();
                return Ok(1);
            }
            catch (Exception e)
            {
                return Ok(0);
                throw;
            }
        }

        // PUT: api/CustomersSync/5
        public void UpdateCustomer(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CustomersSync/5
        public void DeleteCustomer(int id)
        {
        }
    }
}