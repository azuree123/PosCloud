using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels
{
    public class ProductHelperViewModel
    {
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public int StoreId { get; set; }
        public string Barcode { get; set; }
        
    }
}