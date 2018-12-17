

namespace POSApp.Core.Domain
{
    public class Unit : AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        
    }
}
