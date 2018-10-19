using System;
using System.ComponentModel;

namespace POSApp.Core.Models
{
    public class Coupon:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        [DefaultValue(0)]
        public double Value { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        [DefaultValue(0)]
        public int Amount { get; set; }
        public string Days { get; set; }
        [DefaultValue(false)]
        public bool IsPercentage { get; set; }


        
    }
}