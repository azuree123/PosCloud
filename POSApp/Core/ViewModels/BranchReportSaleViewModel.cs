using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class BranchReportSaleViewModel
    {
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
    }
}