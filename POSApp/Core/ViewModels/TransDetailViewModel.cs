using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TransDetailViewModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public int TransMasterId { get; set; }
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        public string CreatedByUserId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

    }
}