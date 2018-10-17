using System.ComponentModel;

namespace POSApp.Core.Models
{
    public class PurchaseOrderDetail:AuditableEntity
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public double Discount { get; set; }
        [DefaultValue(0)]
        public decimal UnitPrice { get; set; }

    }
}