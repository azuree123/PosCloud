using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class StockReportViewModel
    {
        public string ProductCode { get; set; }
        public string BranchName { get; set; }
        public string Name { get; set; }
        public int ReOrderLevel { get; set; }
        public decimal Stock
        {
            get
            {
                return StockIn+OpeningStock+Refunded+OtherIn - (Utilized+Damaged+Wasted+ Transferred + Expired);
            }
        }

        public decimal Utilized { get; set; }
        public decimal StockIn { get; set; }
        public decimal OtherIn { get; set; }
        public decimal Damaged { get; set; }
        public decimal Wasted { get; set; }
        public decimal Transferred { get; set; }
        public decimal Refunded { get; set; }
        public decimal Expired { get; set; }
        public decimal OpeningStock { get; set; }
        public string Unit { get; set; }
    }
}