using System.Collections.Generic;

namespace POSApp.Core.Models
{
    public class ProductCategory:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Product> Products { get; set; }
       

    }
}