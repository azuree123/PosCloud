using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class SalesReportViewModel
    {
        public string InvoiceNumber { get; set; }
        public string SupplierName { get; set; }
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public string Method { get; set; }
        public string OrderType { get; set; }
        public string CustomerName { get; set; }
        public string IngredientUnit { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public DateTime Date { get; set; }
    }
}