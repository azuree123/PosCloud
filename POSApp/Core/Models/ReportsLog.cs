using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ReportsLog:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
      
    }
}