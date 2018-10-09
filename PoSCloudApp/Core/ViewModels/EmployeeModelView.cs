﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.ViewModels
{
    public class EmployeeModelView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public double Salary { get; set; }
        public double Commission { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public DateTime JoinDate { get; set; }
        public bool Booking { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}