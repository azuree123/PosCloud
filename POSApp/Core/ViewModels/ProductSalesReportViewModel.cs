using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductSalesReportViewModel
    {
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
    }
}