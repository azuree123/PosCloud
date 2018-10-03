using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class Employee:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public double Salary { get; set; }
        public double Commission { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
    }
}