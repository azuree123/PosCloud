using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels
{
    public class TimedEventViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }

        public string Type { get; set; }
        [DefaultValue(0)]
        public double Value { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("From Date")]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("To Date")]
        public DateTime ToDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("From Hour")]
        public TimeSpan FromHour { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("To Hour")]
        public TimeSpan ToHour { get; set; }
        [DefaultValue(0)]
        public string[] Days { get; set; }
        [DefaultValue(false)]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DefaultValue(false)]
        [DisplayName("Is Percentage")]
        public bool IsPercentage { get; set; }
        [DefaultValue(false)]
        [DisplayName("Is Taxable")]
        public bool IsTaxable { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DisplayName("Discount Code")]
        public string DiscountCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int[] Categories { get; set; }
        public string[] Products { get; set; }
        public int[] Branches { get; set; }
        [DisplayName("Days Display")]
        public string DaysDisplay { get; set; }
        [DisplayName("Products Display")]
        public string ProductsDisplay { get; set; }
        [DisplayName("Branches Display")]
        public string BranchesDisplay { get; set; }
        public string CategoriesDisplay { get; set; }



        public IEnumerable<SelectListItem> CatDdl { get; set; }
        public IEnumerable<SelectListItem> ProductDdl { get; set; }
        public IEnumerable<SelectListItem> BranchDdl { get; set; }

       
    }
 
}