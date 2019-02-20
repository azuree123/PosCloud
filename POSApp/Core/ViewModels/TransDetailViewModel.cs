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
        
        public decimal? Balance { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        public string CreatedByUserId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DefaultValue(0)]
        public decimal Tax { get; set; }
        public int? DiscountId { get; set; }
        public string Modifiers { get; set; }
        public decimal ModifiersPrice { get; set; }

    }
 
}