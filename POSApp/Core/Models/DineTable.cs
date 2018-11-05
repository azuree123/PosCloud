using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class DineTable:AuditableEntity
    {
        public int Id { get; set; }
        public int DineTableNumber { get; set; }
        public int floorId { get; set; }
        public virtual Floor Floor { get; set; }
    }
}