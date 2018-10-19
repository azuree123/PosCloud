using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApp.Core.Repositories
{
   public interface ICouponRepository
    {
        IEnumerable<Coupon> GetCoupons(int storeid);
        Coupon GetCouponById(int id, int storeid);
        void AddCoupon(Coupon coupon);
        void UpdateCoupon(int id, Coupon coupon, int storeid);
        void DeleteCoupon(int id, int storeid);
    }
}
