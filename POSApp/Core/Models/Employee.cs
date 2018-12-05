using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.Models
{
    public class Employee:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        [Range(0,double.MaxValue)]
        public double Salary { get; set; }
        [Range(0, double.MaxValue)]
        public double Commission { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }

        public byte[] Image { get; set; }

    }

}