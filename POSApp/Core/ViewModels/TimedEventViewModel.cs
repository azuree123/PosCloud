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
        public string Type { get; set; }
        [DefaultValue(0)]
        public double Value { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan FromHour { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan ToHour { get; set; }
        [DefaultValue(0)]
        public string[] Days { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int[] Categories { get; set; }
        public int[] Products { get; set; }
        public int[] Branches { get; set; }

        public string DaysDisplay { get; set; }
        public string ProductsDisplay { get; set; }
        public string BranchesDisplay { get; set; }


        public IEnumerable<SelectListItem> CatDdl { get; set; }
        public IEnumerable<SelectListItem> ProductDdl { get; set; }
        public IEnumerable<SelectListItem> BranchDdl { get; set; }

       
    }
}