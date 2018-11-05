using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TimedEventProducts
    {
        public int TimedEventId { get; set; }
        public int ProductId { get; set; }
        public int ProductStoreId { get; set; }
        public Store ProductStore { get; set; }
        public int TimedEventStoreId { get; set; }
        public Store TimedEventStore { get; set; }
        public TimedEvent TimedEvent { get; set; }
        public Product Product { get; set; }
    }
}