using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TransDetailViewModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public int TransMasterId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public string CreatedByUserId { get; set; }
        public string ProductName { get; set; }

    }
}