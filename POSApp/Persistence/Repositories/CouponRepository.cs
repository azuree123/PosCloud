using POSApp.Core.Models;
using POSApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace POSApp.Persistence.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private PosDbContext _context;

        public CouponRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Coupon> GetCoupons(int storeid)
        {
            return _context.Coupons.Where(a=>a.StoreId==storeid).ToList();
        }

        public Coupon GetCouponById(int id, int storeid)
        {
            return _context.Coupons.Find(id,storeid);
        }

        public void AddCoupon(Coupon coupon)
        {
            if (!_context.Coupons.Where(a => a.Name == coupon.Name  && a.StoreId == coupon.StoreId).Any())
            {
            _context.Coupons.Add(coupon);
            }

        }

        public void UpdateCoupon(int id, Coupon coupon, int storeid)
        {
            if (coupon.Id != id)
            {
                coupon.Id = id;
            }
            else { }

            coupon.StoreId = storeid;
            _context.Coupons.Attach(coupon);
            _context.Entry(coupon).State = EntityState.Modified;
        }

        public void DeleteCoupon(int id, int storeid)
        {
            var coupon = new Coupon { Id = id, StoreId = storeid };
            _context.Coupons.Attach(coupon);
            _context.Entry(coupon).State = EntityState.Deleted;
        }
        public IEnumerable<Coupon> GetApiCoupons()
        {
            IEnumerable<Coupon> coupons = _context.Coupons.Where(a => !a.Synced).ToList();
            foreach (var coupon in coupons)
            {
                coupon.Synced = true;
                coupon.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return coupons;
        }
    }
}