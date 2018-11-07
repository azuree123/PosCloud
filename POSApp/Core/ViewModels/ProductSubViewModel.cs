using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductSubViewModel
    {
        public int ComboProductId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public float Qty { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}