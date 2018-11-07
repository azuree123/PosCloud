using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Discount:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Type { get; set; }//Coupon or Manual
        public string Name { get; set; }
        public string DiscountCode { get; set; }
        public decimal Value { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }


        [DefaultValue(false)]
        public bool IsPercentage { get; set; }
        [DefaultValue(false)]
        public bool IsTaxable { get; set; }
        public string Days { get; set; }
        public bool IsActive { get; set; }

    }
}