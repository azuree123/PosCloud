using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Models
{
    public class Expense:AuditableEntity
    {
        public int Id { get; set; }
        public int ExpenseHead { get; set; }
        public int EmployeeId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public int PurchaseId { get; set; }
        public int VoucherId { get; set; }
        public DateTime Date { get; set; }


    }
}