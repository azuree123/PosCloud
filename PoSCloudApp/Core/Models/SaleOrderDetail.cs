using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class SaleOrderDetail:AuditableEntity
    {
        public int Id { get; set; }
        public int SaleOrderId { get; set; }
        public int ProductId { get; set; }
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public double Price { get; set; }
        [DefaultValue(0)]
        public double Discount { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}