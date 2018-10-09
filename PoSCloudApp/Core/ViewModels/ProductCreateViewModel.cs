using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoSCloudApp.Core.ViewModels
{
    public class ProductCreateViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public string Available { get; set; }
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }
        public double Tax { get; set; }
        public double UnitPrice { get; set; }
        public string Stock { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> CategoryDdl { get; set; }
        public IEnumerable<SelectListItem> SupplierDdl { get; set; } 
    }
}