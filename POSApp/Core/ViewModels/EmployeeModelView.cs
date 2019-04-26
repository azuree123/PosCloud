using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class EmployeeModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        
        public string ArabicName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered email format like (waqar@example.com)")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public string Gender { get; set; }
        
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string MobileNumber { get; set; }

        public double Salary { get; set; }
        [Display(Name = "Commission", ResourceType = typeof(Resource))]
        public double Commission { get; set; }
        [Display(Name = "Shift", ResourceType = typeof(Resource))]
        public int ShiftId { get; set; }
        [Display(Name = "Department", ResourceType = typeof(Resource))]
        public int DepartmentId { get; set; }
        [Display(Name = "Designation", ResourceType = typeof(Resource))]
        public int DesignationId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "JoiningDate", ResourceType = typeof(Resource))]

        public DateTime JoinDate { get; set; }= DateTime.Today;

        public bool Booking { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public IEnumerable<SelectListItem> DepartmentDdl { get; set; }
        public IEnumerable<SelectListItem> ShiftDdl { get; set; }
        public IEnumerable<SelectListItem> DesignationDdl { get; set; }
    }
}