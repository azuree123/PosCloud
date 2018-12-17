

namespace POSApp.Core.Domain
{
    public class City:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }

   
    }
}