using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class IncrementalSyncronization
    {
        public int  Id { get; set; }
        public DateTime LastSynced { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}