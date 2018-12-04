using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.Models
{
    public class City:AuditableEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}