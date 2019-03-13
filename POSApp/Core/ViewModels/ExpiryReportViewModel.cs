using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ExpiryReportViewModel
    {
        public string BranchName { get; set; }
        public string Products { get; set; }
        public string TransCode { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
  
        public DateTime Date { get; set; }
    }
}