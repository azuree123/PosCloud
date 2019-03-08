using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TransDetail:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        [DefaultValue(0)]
        public decimal Tax { get; set; }
        public int TransMasterId { get; set; }
        public TransMaster TransMaster { get; set; }
        public string ProductCode { get; set; }
        public Product Product { get; set; }

        [DefaultValue(0)]
        public decimal Quantity { get; set; }
        [DefaultValue(0)]
        public decimal UnitPrice { get; set; }

        public int? DiscountId { get; set; }
        [DefaultValue(0)]
        public decimal Balance { get; set; }

        public bool Waste { get; set; }
        public virtual TimedEvent TimedEvent { get; set; }

        [DefaultValue(0)]
        public decimal Discount { get; set; }
        public virtual ICollection<ModifierTransDetail> ModifierTransDetail { get; set; }
    }
}