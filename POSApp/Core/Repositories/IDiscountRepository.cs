using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IDiscountRepository
    {
        Discount GetDiscount(int id);
        IEnumerable<Discount> GetDiscounts(int storeId);
        IEnumerable<Discount> GetDiscountsFiltered(string query, int storeId);
        IEnumerable<Discount> GetDiscountsFiltered(int query, int storeId);
        int IsExisting(string discountName, int storeId);
        void Delete(int id, int storeId);
        void Add(Discount optcategory);
    }
}
