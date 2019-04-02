using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Warehouse:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<TransMaster> TransMasters { get; set; } = new List<TransMaster>();
    }
}