using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class DineTable:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public string DineTableNumber { get; set; }
        public bool IsBooked { get; set; }
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual ICollection<TransMaster> TransMasters { get; set; }
    }
}