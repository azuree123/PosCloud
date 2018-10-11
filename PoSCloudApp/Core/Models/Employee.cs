﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class Employee:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        [Range(0,double.MaxValue)]
        public double Salary { get; set; }
        [Range(0, double.MaxValue)]
        public double Commission { get; set; }
        public DateTime JoinDate { get; set; }
        public bool Booking { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        public string Address { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
 
}