using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ProductSubViewModel
    {
        public string ComboProductCode { get; set; }
        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }
        [Display(Name = "Modifiable", ResourceType = typeof(Resource))]
        public bool Modifiable { get; set; }
        public int StoreId { get; set; }
        [Display(Name = "product", ResourceType = typeof(Resource))]
        public string ProductCode { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public float Qty { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ProductName { get; set; }
        public List<SelectListItem> ProductDdl { get; set; }
    }
}