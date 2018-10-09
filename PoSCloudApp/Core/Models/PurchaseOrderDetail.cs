using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class PurchaseOrderDetail:AuditableEntity
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double RetailPrice { get; set; }
        public double Discount { get; set; }
        public double UnitPrice { get; set; }

    }
}