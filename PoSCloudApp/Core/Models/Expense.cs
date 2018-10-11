using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Models
{
    public class Expense:AuditableEntity
    {
        public int Id { get; set; }
        public int ExpenseHeadId { get; set; }
        public virtual ExpenseHead ExpenseHead { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Range(0,double.MaxValue)]
        public double Amount { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


    }
}