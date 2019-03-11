using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class PurchaseReportViewModel
    {
        public string InvoiceNumber { get; set; }
        public string BranchName { get; set; }
        public string SupplierName { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string  Unit { get; set; }
        public decimal Tax { get; set; }
        public DateTime Date { get; set; }
    }
}