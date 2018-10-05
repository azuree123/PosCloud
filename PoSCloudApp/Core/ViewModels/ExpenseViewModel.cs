using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.ViewModels
{
    public class ExpenseViewModel
    {
        public int? Id { get; set; }
        public int ExpenseHeadId { get; set; }
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}