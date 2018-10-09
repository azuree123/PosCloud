using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class Product:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public string Available { get; set; }
        public int SupplierId { get; set; }
        public double Tax { get; set; }
        public double UnitPrice { get; set; }
        public string Stock { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    }
}