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

        public IEnumerable<TimedEventProducts> GeTimedEventProducts(int storeId)
        {
            return _context.TimedEventProducts.Where(a=>a.StoreId == storeId).ToList();
        }

        public TimedEventProducts GetTimedEventsById(int id, int storeId)
        {
            return _context.TimedEventProducts.Find(id, storeId);
        }

        public void AddTimedEventProducts(TimedEventProducts tep)
        {
            if (!_context.TimedEventProducts.Where(a => a.ProductId == tep.ProductId && a.StoreId == tep.StoreId).Any())
            {
                _context.TimedEventProducts.Add(tep);
            }
        }

        public void UpdateTimedEventProducts(int id,int timedEventId, TimedEventProducts tep ,int storeId)
        {
            if (tep.ProductId != id)
            {
                tep.ProductId = id;
            }
            else { }
            if (tep.TimedEventId != timedEventId)
            {
                tep.TimedEventId = timedEventId;
            }
            else { }
            tep.StoreId = storeId;
            _context.TimedEventProducts.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteTimedEventProducts(int id, int storeId)
        {
            var tep = new TimedEventProducts { StoreId = storeId };
            _context.TimedEventProducts.Attach(tep);
            _context.Entry(tep).State = EntityState.Deleted;
        }
    }
}