using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TaxReportViewModel
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public bool Percentage { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}