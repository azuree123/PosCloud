using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductSizeOrderTypeReportViewModel
    {
        public string ProductName { get; set; }
        public string BranchName { get; set; }
        public string Size { get; set; }
        public decimal Qty { get; set; }
        public string OrderType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}