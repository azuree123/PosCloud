using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class EmployeeModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public double Salary { get; set; }
        public double Commission { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public bool Booking { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public IEnumerable<SelectListItem> DepartmentDdl { get; set; }
        public IEnumerable<SelectListItem> DesignationDdl { get; set; }
    }
}