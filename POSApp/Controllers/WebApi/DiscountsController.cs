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
        public async Task<IHttpActionResult> GetDiscounts()
        {
            return Ok(Mapper.Map<DiscountViewModel[]>(_unitOfWork.DiscountRepository.GetApiDiscounts()));
        }

        // GET: api/DiscountsSync/5
        public async Task<IHttpActionResult> GetDiscount(int id, int storeId)
        {
            return Ok(_unitOfWork.DiscountRepository.GetDiscountById(id,storeId));
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
                    _unitOfWork.DiscountRepository.AddDiscount(discount);
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