using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ComboProductsTransDetail
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int TransDetailId { get; set; }
       
        public TransDetail TransDetail { get; set; }
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public decimal UnitPrice { get; set; }
        public int ProductSubId { get; set; }
        public ProductsSub ProductsSub { get; set; }
    }
}