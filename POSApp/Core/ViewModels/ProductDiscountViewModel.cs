using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductDiscountViewModel
    {
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }

    }
}