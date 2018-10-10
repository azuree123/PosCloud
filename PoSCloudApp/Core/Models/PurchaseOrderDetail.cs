using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public double RetailPrice { get; set; }
        [DefaultValue(0)]
        public double Discount { get; set; }
        [DefaultValue(0)]
        public double UnitPrice { get; set; }

    }
}