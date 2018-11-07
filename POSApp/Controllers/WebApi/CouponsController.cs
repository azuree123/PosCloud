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
    public class CouponsController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public CouponsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetCoupons(int storeId)
        {
            return Ok(Mapper.Map<CouponModelView[]>(_unitOfWork.CouponRepository.GetCoupons(storeId)));
        }

        // GET: api/CouponsSync/5
        public async Task<IHttpActionResult> GetCoupon(int id,int storeId)
        {
            return Ok(_unitOfWork.CouponRepository.GetCouponById(id,storeId));
        }

        // POST: api/CouponsSync
        public async Task<IHttpActionResult> AddCoupons([FromBody]SyncObject sync)
        {
            try
            {
                List<Coupon> coupons = System.Web.Helpers.Json.Decode<List<Coupon>>(sync.Object);
                foreach (var coupon in coupons)
                {
                    coupon.Code = coupon.Id.ToString();
                    coupon.Synced = true;
                    coupon.SyncedOn = DateTime.Now;
                    _unitOfWork.CouponRepository.AddCoupon(coupon);
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

        // PUT: api/CouponsSync/5
        public void UpdateCoupon(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CouponsSync/5
        public void DeleteCoupon(int id)
        {
        }
    }
}
