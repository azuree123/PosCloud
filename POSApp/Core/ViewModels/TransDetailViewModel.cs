using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class TransDetailViewModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public int TransMasterId { get; set; }
        [DisplayName("Product Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Balance", ResourceType = typeof(Resource))]
        public decimal? Balance { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public decimal Quantity { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resource))]
        public decimal Discount { get; set; }
        [Display(Name = "UnitPrice", ResourceType = typeof(Resource))]
        public decimal UnitPrice { get; set; }
        public string CreatedByUserId { get; set; }
        [Display(Name = "Products", ResourceType = typeof(Resource))]
        public string ProductName { get; set; }
        [DefaultValue(0)]
        [Display(Name = "Tax", ResourceType = typeof(Resource))]
        public decimal Tax { get; set; }
        public int? DiscountId { get; set; }
        [Display(Name = "Modifiers", ResourceType = typeof(Resource))]
        public string Modifiers { get; set; }

        public decimal ModifiersPrice { get; set; }
        [Display(Name = "Unit", ResourceType = typeof(Resource))]
        public string UnitName { get; set; }

        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
    }
 
}