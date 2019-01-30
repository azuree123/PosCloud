using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class BranchTimelyReportViewModel
    {
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public TimeSpan Time { get; set; }
    }
}