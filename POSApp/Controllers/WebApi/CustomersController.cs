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

namespace POSApp.Controllers.WebApi
{
    public class CustomersController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetCustomers()
        {
            return Ok(_unitOfWork.CustomerRepository.GetApiCustomers());
        }

        // GET: api/CustomersSync/5
        public async Task<IHttpActionResult> GetCustomer(int id, int storeId)
        {
            return Ok(_unitOfWork.CustomerRepository.GetCustomerById(id, storeId));
        }

        // POST: api/CustomersSync
        public async Task<IHttpActionResult> AddCustomers([FromBody]SyncObject sync)
        {
            try
            {
                List<Customer> customers = System.Web.Helpers.Json.Decode<List<Customer>>(sync.Object);
                foreach (var customer in customers)
                {
                    customer.Code = customer.Id.ToString();
                    _unitOfWork.CustomerRepository.AddCustomer(customer);
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