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
    public class TimedEventViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        public string ArabicName { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resource))]

        public string Type { get; set; }
        [DefaultValue(0)]
        [Display(Name = "Value", ResourceType = typeof(Resource))]
        public double Value { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "DateForm", ResourceType = typeof(Resource))]
        public DateTime FromDate { get; set; } = DateTime.Today;
        [DataType(DataType.Date)]
        [Display(Name = "Dateto", ResourceType = typeof(Resource))]
        public DateTime ToDate { get; set; } = DateTime.Today;
        [DataType(DataType.Time)]
        [Display(Name = "FromHour", ResourceType = typeof(Resource))]
        public TimeSpan FromHour { get; set; }= new TimeSpan();
        [DataType(DataType.Time)]
        [Display(Name = "ToHour", ResourceType = typeof(Resource))]
        public TimeSpan ToHour { get; set; } = new TimeSpan();
        [DefaultValue(0)]
        [Display(Name = "Days", ResourceType = typeof(Resource))]
        public string[] Days { get; set; }
        [DefaultValue(false)]
        [Display(Name = "IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        [Display(Name = "IsPercentage", ResourceType = typeof(Resource))]
        public bool IsPercentage { get; set; }
        [DefaultValue(false)]
        [Display(Name = "IsTaxable", ResourceType = typeof(Resource))]
        public bool IsTaxable { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "DiscountCode", ResourceType = typeof(Resource))]
        public string DiscountCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Categories", ResourceType = typeof(Resource))]
        public int[] Categories { get; set; }
        [Display(Name = "Products", ResourceType = typeof(Resource))]
        public string[] Products { get; set; }
        [Display(Name = "Branches", ResourceType = typeof(Resource))]
        public int[] Branches { get; set; }
        [Display(Name = "Days", ResourceType = typeof(Resource))]
        public string DaysDisplay { get; set; }
        [Display(Name = "Products", ResourceType = typeof(Resource))]
        public string ProductsDisplay { get; set; }
        [Display(Name = "Branches", ResourceType = typeof(Resource))]
        public string BranchesDisplay { get; set; }
        [Display(Name = "Categories", ResourceType = typeof(Resource))]
        public string CategoriesDisplay { get; set; }



        public IEnumerable<SelectListItem> CatDdl { get; set; }
        public IEnumerable<SelectListItem> ProductDdl { get; set; }
        public IEnumerable<SelectListItem> BranchDdl { get; set; }

       
    }
 
}