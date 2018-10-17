using System;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.Models
{
    public class Expense:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
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