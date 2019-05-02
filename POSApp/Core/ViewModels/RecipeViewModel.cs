using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Core.Models;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class RecipeViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        [Display(Name = "Productcode", ResourceType = typeof(Resource))]
        public string ProductCode { get; set; }
        [Display(Name = "Ingredient", ResourceType = typeof(Resource))]
        public string IngredientCode { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public decimal Quantity { get; set; }
        [Display(Name = "Calories", ResourceType = typeof(Resource))]
        public decimal? Calories { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "ExpiryDate", ResourceType = typeof(Resource))]
        public DateTime ExpiryDate { get; set; } = DateTime.Today;
        public IEnumerable<SelectListItem> ProductDDl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }

        public string ProductsDisplay { get; set; }
        public string[] Products { get; set; }

        public List<RecipeListViewModel> RecipeList { get; set; }
        
    }
    public class RecipeListViewModel
    {
        [Display(Name = "Productcode", ResourceType = typeof(Resource))]
        public string ProductCode { get; set; }
        public int? Id { get; set; }
        public int StoreId { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public decimal Quantity { get; set; }
        [Display(Name = "ExpiryDate", ResourceType = typeof(Resource))]
        public DateTime ExpiryDate { get; set; }
        [Display(Name = "Calories", ResourceType = typeof(Resource))]
        public decimal? Calories { get; set; }
        [Display(Name = "Ingredient", ResourceType = typeof(Resource))]
        public string IngredientName { get; set; }
        [Display(Name = "Unit", ResourceType = typeof(Resource))]
        public string Unit { get; set; }
       


    }

}