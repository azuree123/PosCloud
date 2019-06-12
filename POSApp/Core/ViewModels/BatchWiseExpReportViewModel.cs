using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class BatchWiseExpReportViewModel
    {

        public string BranchName { get; set; }
        public string Products { get; set; }
        public string TransCode { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ManufactureDate { get; set; } 
        public DateTime ExpiryDate { get; set; }


       
    }
}