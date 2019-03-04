using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductCostReportViewModel
    {
        public string ProductName { get; set; }
        public double CostPrice { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateTime { get; set; }
    }
}