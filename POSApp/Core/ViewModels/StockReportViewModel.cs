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

        public decimal Stock
        {
            get
            {
                return Purchased+OpeningStock+Refunded-(Utilized+Damaged+Wasted+ Transfered+Expired);
            }
        }

        public decimal Utilized { get; set; }
        public decimal Purchased { get; set; }

        public decimal Damaged { get; set; }
        public decimal Wasted { get; set; }
        public decimal Transfered { get; set; }
        public decimal Refunded { get; set; }
        public decimal Expired { get; set; }
        public decimal OpeningStock { get; set; }
    }
}