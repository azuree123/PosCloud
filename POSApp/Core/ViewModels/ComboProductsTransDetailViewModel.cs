using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ComboProductsTransDetailViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int TransDetailId { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public int Quantity { get; set; }
        [Display(Name = "UnitPrice", ResourceType = typeof(Resource))]
        public decimal UnitPrice { get; set; }
    }
}