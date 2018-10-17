using System.ComponentModel;

namespace POSApp.Core.Models
{
    public class SaleOrderDetail:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }


        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        

        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public decimal UnitPrice { get; set; }
        [DefaultValue(0)]
        public double Discount { get; set; }
       
        
    }
}