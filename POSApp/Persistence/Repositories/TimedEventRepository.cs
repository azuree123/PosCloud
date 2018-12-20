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
            return _context.TimedEvents.Include(a => a.TimedEventProducts).Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }

        public TimedEvent GetTimedEventById(int id, int storeid)
        {
            return _context.TimedEvents.Include(a=>a.TimedEventProducts).FirstOrDefault(a=>a.Id==id&& a.StoreId== storeid);
        }

        public void AddTimedEvent(TimedEvent TimedEvent)
        {
            var inDb = _context.TimedEvents.FirstOrDefault(a => a.Name == TimedEvent.Name && a.StoreId == TimedEvent.StoreId);
            if (inDb == null)
            {
                _context.TimedEvents.Add(TimedEvent);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    TimedEvent.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(TimedEvent);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
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
            var timedEvent = _context.TimedEvents.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            timedEvent.IsDisabled = true;
            _context.TimedEvents.Attach(timedEvent);
            _context.Entry(timedEvent).State = EntityState.Modified;
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