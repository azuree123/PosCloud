using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class TimedEventProductsRepository:ITimedEventProductsRepository
    {
        private PosDbContext _context;

        public TimedEventProductsRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TimedEventProducts> GeTimedEventProducts(int id, int storeId)
        {
            return _context.TimedEventProducts.Where(a=>a.StoreId == storeId && a.TimedEventId==id).ToList();
        }

        public TimedEventProducts GetTimedEventsById(int id, int storeId,string product)
        {
            return _context.TimedEventProducts.FirstOrDefault(a=>a.StoreId==storeId&&a.ProductCode==product&&a.TimedEventId==id);
        }

        public void AddTimedEventProducts(TimedEventProducts tep)
        {
            
            if (!_context.TimedEventProducts.Where(a => a.ProductCode == tep.ProductCode && a.StoreId == tep.StoreId && a.TimedEventId == tep.TimedEventId).Any())
            {
                _context.TimedEventProducts.Add(tep);
            }
        }

        public void UpdateTimedEventProducts(string id,int timedEventId, TimedEventProducts tep ,int storeId)
        {
            if (tep.ProductCode != id)
            {
                tep.ProductCode = id;
            }
            else { }
            if (tep.TimedEventId != timedEventId)
            {
                tep.TimedEventId = timedEventId;
            }
            else { }
            tep.StoreId = storeId;
          //  tep.TimedEventStoreId = storeId;

            _context.TimedEventProducts.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteTimedEventProducts(int id, int storeId)
        {
            List<TimedEventProducts> products = _context.TimedEventProducts
                .Where(a => a.StoreId == storeId && a.TimedEventId == id).ToList();
            foreach (var timedEventProductse in products)
            {
                
                _context.TimedEventProducts.Remove(timedEventProductse);
            }
          
        }
    }
}