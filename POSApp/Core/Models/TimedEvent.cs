using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TimedEvent:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [DefaultValue(0)]
        public double Value { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan FromHour { get; set; }
        public TimeSpan ToHour { get; set; }
        [DefaultValue(0)]
        public string Days { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}