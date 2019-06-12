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
    public class TaxesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public TaxesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetTax(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.TaxRepository.GetTaxesAsync(storeId);


                return Ok(Mapper.Map<TaxViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Tax");
                if (lastSync == null)
                {
                    data = await _unitOfWork.TaxRepository.GetTaxesAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.TaxRepository.GetAllTaxesAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Tax"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<TaxViewModel[]>(data));


            }


        }

        // GET: api/TaxCategoriesSync/5
        public async Task<IHttpActionResult> GetTax(int id, int storeId)
        {
            return Ok(await _unitOfWork.TaxRepository.GetTaxByIdAsync(id,storeId));
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
                    tax.Synced = true;
                    tax.SyncedOn = DateTime.Now;
                    await _unitOfWork.TaxRepository.AddTaxAsync(tax);
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
