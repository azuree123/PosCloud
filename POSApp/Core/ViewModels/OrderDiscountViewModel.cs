using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class OrderDiscountViewModel
    {
        public string BranchName { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}