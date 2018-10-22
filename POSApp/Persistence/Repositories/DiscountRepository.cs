using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly PosDbContext _context;

        public DiscountRepository(PosDbContext context)
        {
            _context = context;
        }
        public Discount GetDiscount(int id)
        {
            return _context.Discounts.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Discount> GetDiscounts(int storeId)
        {
            //return _context.Discount;
            _context.Discounts.Load();
            return _context.Discounts
                .Local.ToBindingList()
                .Where(a => a.StoreId == storeId)
                .ToList();
        }

        public IEnumerable<Discount> GetDiscountsFiltered(string query, int storeId)
        {
            //return _context.Discount;
            query = query.ToUpper();
            return _context.Discounts.Local
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                ;
        }
        public IEnumerable<Discount> GetDiscountsFiltered(int query, int storeId)
        {
            return _context.Discounts.Local
                    .Where(x => x.Id == query && x.StoreId == storeId)
                ;
        }

        public int IsExisting(string discountName, int storeId)
        {
            var discount = _context.Discounts.Where(z => z.Name == discountName && z.StoreId == storeId);
            if (discount.Any())
            {
                return discount.FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }

        }
        public void Delete(int id, int storeId)
        {
            var dept = _context.Discounts.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            _context.Discounts.Remove(dept);
        }
        public void Add(Discount optcategory)
        {
            _context.Discounts.Add(optcategory);

        }
    }
}