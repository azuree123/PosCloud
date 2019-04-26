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
        public string ProductCode { get; set; }
        [DisplayName("Ingredient")]
        public string IngredientCode { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public decimal Quantity { get; set; }
        
        public decimal? Calories { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        public IEnumerable<SelectListItem> ProductDDl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }
        public string ProductsDisplay { get; set; }
        public string[] Products { get; set; }

        public List<RecipeListViewModel> RecipeList { get; set; }
        
    }
    public class RecipeListViewModel
    {
        public string ProductCode { get; set; }
        public int? Id { get; set; }
        public int StoreId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal? Calories { get; set; }
        public string IngredientName { get; set; }
        public string Unit { get; set; }
       


    }

}