using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Dtos
{
    public class PosProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int StoreId { get; set; }
        public string ProductImage { get; set; }
    }
}