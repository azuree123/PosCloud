using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.Models
{
    public class Department:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}