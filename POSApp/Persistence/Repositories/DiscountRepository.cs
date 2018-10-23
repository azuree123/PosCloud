using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public Discount GetDiscountById(int id, int storeId)
        {
            return _context.Discounts.FirstOrDefault(x => x.Id == id && x.StoreId==storeId);
        }
        public IEnumerable<Discount> GetDiscounts(int storeId)
        {
            //return _context.Discount;
            return _context.Discounts
                .Where(a => a.StoreId == storeId)
                .ToList();
        }

        public IEnumerable<Discount> GetDiscountsFiltered(string query, int storeId)
        {
            //return _context.Discount;
            query = query.ToUpper();
            return _context.Discounts
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                ;
        }
        public IEnumerable<Discount> GetDiscountsFiltered(int query, int storeId)
        {
            return _context.Discounts
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
        public void DeleteDiscount(int id, int storeId)
        {
            var discounted = new Discount { Id = id, StoreId = storeId };
            _context.Discounts.Attach(discounted);
            _context.Entry(discounted).State = EntityState.Deleted;
        }
        public void AddDiscount(Discount optcategory)
        {
            _context.Discounts.Add(optcategory);

        }

        public void UpdateDiscount(int id, Discount discount, int storeId)
        {
            if (discount.Id != id)
            {
                discount.Id = id;
            }
            else { }

            discount.StoreId = storeId;
            _context.Discounts.Attach(discount);
            _context.Entry(discount).State = EntityState.Modified;
        }
        public IEnumerable<Discount> GetApiDiscounts()
        {
            IEnumerable<Discount> discounts = _context.Discounts.Where(a => !a.Synced).ToList();
            foreach (var discount in discounts)
            {
                discount.Synced = true;
                discount.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return discounts;
        }
    }
}