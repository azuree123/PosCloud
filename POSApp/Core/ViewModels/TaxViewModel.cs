using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TaxViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(0)]
        public double Rate { get; set; }
        [DefaultValue(0)]
        public bool IsPercentage { get; set; }
        public int? StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
    }
}