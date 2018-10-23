using System.Collections.Generic;
using System.ComponentModel;

namespace POSApp.Core.Models
{
    public class SaleOrder:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        [DefaultValue(0)]
        public double Amount { get; set; }
        [DefaultValue(0)]
        public double Tax { get; set; }
        [DefaultValue(0)]
        public double Discount { get; set; }
        public string Status { get; set; }
        [DefaultValue(false)]
        public bool Canceled { get; set; }
        public string Type { get; set; }
       
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }

    }
}