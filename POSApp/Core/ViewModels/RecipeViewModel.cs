using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class RecipeViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        public string ProductCode { get; set; }
        public string IngredientCode { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public decimal? Calories { get; set; }
        public string Code { get; set; }
        public string ProductsDisplay { get; set; }
        public string[] Products { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> ProductDDl { get; set; }
        public IEnumerable<SelectListItem> RecipeProductDDl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }
    }
}