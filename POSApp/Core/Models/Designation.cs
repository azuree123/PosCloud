using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class Designation:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}