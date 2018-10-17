using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class State :AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}