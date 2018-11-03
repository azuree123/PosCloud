using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface ITimedEventRepository
    {
        IEnumerable<TimedEvent> GetTimedEvents(int storeid);
        TimedEvent GetTimedEventById(int id, int storeid);
        void AddTimedEvent(TimedEvent timedEvent);
        void UpdateTimedEvent(int id, TimedEvent timedEvent, int storeid);
        void DeleteTimedEvent(int id, int storeid);
        IEnumerable<TimedEvent> GetApiTimedEvents();
    }
}
