using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class Supplier:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

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