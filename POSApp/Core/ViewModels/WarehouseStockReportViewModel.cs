using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class WarehouseStockReportViewModel
    {
        public string ProductCode { get; set; }

        public string Name { get; set; }

        public decimal Stock
        {
            get
            {
                return (Purchased + StockTaking) - StockIn;
            }
        }


        public decimal Purchased { get; set; }
        public decimal StockTaking { get; set; }
        public decimal StockIn { get; set; }
        public string PurchaseUnit { get; set; }
    }
}