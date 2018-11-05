using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.Models
{
    public class DineTable:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int DineTableNumber { get; set; }
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual ICollection<TransMaster> TransMasters { get; set; }
        public IEnumerable<SelectListItem> FloorDdl { get; set; }
    }
}