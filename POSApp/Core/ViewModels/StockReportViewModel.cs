using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class StockReportViewModel
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public double Stock { get; set; }
        public decimal Sale { get; set; }
        public int Purchase { get; set; }
    }
}