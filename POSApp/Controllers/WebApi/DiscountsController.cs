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
    public class DiscountsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public DiscountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public async Task<IHttpActionResult> GetDiscounts(int storeId)
        //{
        //    return Ok(Mapper.Map<DiscountViewModel[]>(await _unitOfWork.DiscountRepository.GetDiscountsAsync(storeId)));
        //}
        public async Task<IHttpActionResult> GetDiscounts(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.DiscountRepository.GetDiscountsAsync(storeId);


                return Ok(Mapper.Map<DiscountViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Discounts");
                if (lastSync == null)
                {
                    data = await _unitOfWork.DiscountRepository.GetDiscountsAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.DiscountRepository.GetAllDiscountsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Discounts"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<DiscountViewModel[]>(data));


            }


        }
        // GET: api/DiscountsSync/5
        public async Task<IHttpActionResult> GetDiscount(int id, int storeId)
        {
            return Ok(await _unitOfWork.DiscountRepository.GetDiscountByIdAsync(id,storeId));
        }

        // POST: api/DiscountsSync
        public async Task<IHttpActionResult> AddDiscounts([FromBody]SyncObject sync)
        {
            try
            {
                List<Discount> discounts = System.Web.Helpers.Json.Decode<List<Discount>>(sync.Object);
                foreach (var discount in discounts)
                {
                    discount.Code = discount.Id.ToString();
                    discount.Synced = true;
                    discount.SyncedOn = DateTime.Now;
                    await _unitOfWork.DiscountRepository.AddDiscountAsync(discount);
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

        // PUT: api/DiscountsSync/5
        public void UpdateDiscount(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DiscountsSync/5
        public void DeleteDiscount(int id)
        {
        }
    }
}