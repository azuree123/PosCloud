using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ProductsSub:AuditableEntity
    {
        public string ComboProductCode { get; set; }
        public Product ComboProduct { get; set; }
        public decimal Price { get; set; }
        public bool Modifiable { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string ProductCode { get; set; }
        public Product Product { get; set; }
        [DefaultValue(0)]
        public float Qty { get; set; }

        public virtual ICollection<ComboProductsTransDetail> ComboProductsTransDetails { get; set; }

    }
}