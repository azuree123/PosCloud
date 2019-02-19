using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POSApp.Persistence;

namespace POSApp.Core.Models
{
       
    public class Client:AuditableEntity
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Address { get; set; }
        
        public string Contact { get; set; }
        
        
        public string City { get; set; }
        
        public string State { get; set; }
        public virtual ICollection<Store> Stores { get; set; }

    }
}