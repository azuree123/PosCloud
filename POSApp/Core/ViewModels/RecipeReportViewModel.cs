using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class RecipeReportViewModel
    {
        public string IngredientCode { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Calories { get; set; }
        public string IngredientName { get; set; }
        public string Unit { get; set; }
    }
}