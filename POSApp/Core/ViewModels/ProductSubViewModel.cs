using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class ProductSubViewModel
    {
        public string ComboProductCode { get; set; }
        public decimal Price { get; set; }
        public bool Modifiable { get; set; }
        public int StoreId { get; set; }
        [DisplayName("Product")]
        public string ProductCode { get; set; }
        [DisplayName("Quantity")]
        public float Qty { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ProductName { get; set; }
        public List<SelectListItem> ProductDdl { get; set; }
    }
}