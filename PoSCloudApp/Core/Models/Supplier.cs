using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class Supplier:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }
        public string CpMobileNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        
        public string State { get; set; }
        public string City { get; set; }
        
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }


    }
}