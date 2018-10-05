using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoSCloudApp.Core.ViewModels
{
    public class ExpenseViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Select Expense Head")]
        public int ExpenseHeadId { get; set; }
        [DisplayName("Select Employee")]
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> EmpDdl { get; set; }
        public IEnumerable<SelectListItem> ExpHeadDdl { get; set; }

    }
}