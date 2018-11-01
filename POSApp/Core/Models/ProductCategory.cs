using System.Collections.Generic;
using System.Drawing;

namespace POSApp.Core.Models
{
    public class ProductCategory:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Product> Products { get; set; }
       

    }
}