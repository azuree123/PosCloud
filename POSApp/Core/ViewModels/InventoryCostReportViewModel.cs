using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class InventoryCostReportViewModel
    {
        
        public string ItemName { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }

    }
}