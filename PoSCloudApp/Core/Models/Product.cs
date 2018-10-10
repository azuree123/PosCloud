using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(0)]
        public double Tax { get; set; }
        [DefaultValue(0)]
        public double UnitPrice { get; set; }
        [DefaultValue(0)]
        public double Stock { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }

    }
}