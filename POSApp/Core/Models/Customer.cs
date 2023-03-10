using System;
using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class Customer:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }


        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public string Referral { get; set; }
       // public string Gender { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }

    
}