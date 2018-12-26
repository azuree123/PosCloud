using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class BusinessPartner:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Type { get; set; }
        
        public string Name { get; set; }
       
       
        public string PhoneNumber { get; set; }
        
       
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
       
      
        public string ContactPerson { get; set; }
        public string CpMobileNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string Remarks { get; set; }

        
        public ICollection<TransMaster> TransMasters
        {
            get;
            set;
        }
    }
}