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
    public class TaxesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public TaxesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetTaxes()
        {
            return Ok(_unitOfWork.TaxRepository.GetApiTaxes());
        }

        // GET: api/TaxCategoriesSync/5
        public async Task<IHttpActionResult> GetTax(int id, int storeId)
        {
            return Ok(_unitOfWork.TaxRepository.GetTaxById(id,storeId));
        }

        // POST: api/TaxCategoriesSync
        public async Task<IHttpActionResult> AddTaxes([FromBody]SyncObject sync)
        {
            try
            {
                List<Tax> taxes = System.Web.Helpers.Json.Decode<List<Tax>>(sync.Object);
                foreach (var tax in taxes)
                {
                    tax.Code = tax.Id.ToString();
                    _unitOfWork.TaxRepository.AddTax(tax);
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

        // PUT: api/TaxCategoriesSync/5
        public void UpdateTax(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TaxCategoriesSync/5
        public void DeleteTax(int id)
        {
        }
    }
}
