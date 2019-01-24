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
        Discount GetDiscountById(int id,int storeId);
        IEnumerable<Discount> GetDiscounts(int storeId);
        IEnumerable<Discount> GetDiscountsFiltered(string query, int storeId);
        IEnumerable<Discount> GetDiscountsFiltered(int query, int storeId);
        int IsExisting(string discountName, int storeId);
        void DeleteDiscount(int id, int storeId);
        void AddDiscount(Discount optcategory);
        void UpdateDiscount(int id, Discount discount, int storeId);
        IEnumerable<Discount> GetApiDiscounts();
        Task<Discount> GetDiscountByIdAsync(int id, int storeId);
        Task<IEnumerable<Discount>> GetDiscountsAsync(int storeId);
        Task AddDiscountAsync(Discount optcategory);
    }
}
