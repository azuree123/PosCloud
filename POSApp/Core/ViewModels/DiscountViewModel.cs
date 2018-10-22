using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class DiscountViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(0)]
        public double Amount { get; set; }
        [DefaultValue(false)]
        public bool IsPercentage { get; set; }
        [DefaultValue(false)]
        public bool IsTaxable { get; set; }
        public int StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}