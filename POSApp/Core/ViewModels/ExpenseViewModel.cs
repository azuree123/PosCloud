using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ExpenseViewModel
    {
        public int? Id { get; set; }
        
        public int? StoreId { get; set; }
        [Display(Name = "ExpenseHead", ResourceType = typeof(Resource))]
        public int ExpenseHeadId { get; set; }
        [Display(Name = "Employee", ResourceType = typeof(Resource))]
        public int EmployeeId { get; set; }
        [Range(1,10000000000000000000, ErrorMessage = "Ammount must be between $1 and $100")]
        [Display(Name = "amount", ResourceType = typeof(Resource))]
        public double Amount { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Date", ResourceType = typeof(Resource))]
        public DateTime Date { get; set; }= DateTime.Today;
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public IEnumerable<SelectListItem> EmpDdl { get; set; }
        public IEnumerable<SelectListItem> StoreDdl { get; set; }
        public IEnumerable<SelectListItem> ExpHeadDdl { get; set; }
        public SelectList Employee { get; set; }

    }
   

}