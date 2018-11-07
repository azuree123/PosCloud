using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class TimedEventRepository : ITimedEventRepository
    {
        private PosDbContext _context;

        public TimedEventRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TimedEvent> GetTimedEvents(int storeid)
        {
            return _context.TimedEvents.Where(a => a.StoreId == storeid).ToList();
        }

        public TimedEvent GetTimedEventById(int id, int storeid)
        {
            return _context.TimedEvents.Include(a=>a.TimedEventProducts).FirstOrDefault(a=>a.Id==id&& a.StoreId== storeid);
        }

        public void AddTimedEvent(TimedEvent TimedEvent)
        {
            if (!_context.TimedEvents.Where(a => a.Name == TimedEvent.Name && a.StoreId == TimedEvent.StoreId).Any())
            {
                _context.TimedEvents.Add(TimedEvent);
            }

        }

        public void UpdateTimedEvent(int id, TimedEvent timedEvent, int storeid)
        {
            if (timedEvent.Id != id)
            {
                timedEvent.Id = id;
            }
            else { }

            timedEvent.StoreId = storeid;
            _context.TimedEvents.Attach(timedEvent);
            _context.Entry(timedEvent).State = EntityState.Modified;
        }

        public void DeleteTimedEvent(int id, int storeid)
        {
            var timedEvent = new TimedEvent { Id = id, StoreId = storeid };
            _context.TimedEvents.Attach(timedEvent);
            _context.Entry(timedEvent).State = EntityState.Deleted;
        }
        public IEnumerable<TimedEvent> GetApiTimedEvents()
        {
            IEnumerable<TimedEvent> timedEvents = _context.TimedEvents.Where(a => !a.Synced).ToList();
            foreach (var timedEvent in timedEvents)
            {
                timedEvent.Synced = true;
                timedEvent.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return timedEvents;
        }
    }
}