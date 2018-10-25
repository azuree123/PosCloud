using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class InvoiceViewModel
    {
        public string TransCode { get; set; }
        //public int StoreId { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransStatus { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Product { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }
        public string Size { get; set; }
        public string Customer { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string UserName { get; set; }
    }
}