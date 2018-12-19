using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ModifierTransDetailViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int TransDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ModifierOptionId { get; set; }
    }
}