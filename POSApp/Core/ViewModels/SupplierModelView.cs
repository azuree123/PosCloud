using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.ViewModels
{
    public class SupplierModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered email format like (waqar@gmail.com)")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Entered phone format like (03211234567)")]
        public string PhoneNumber { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("CP Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Entered phone format like (03211234567)")]
        public string CpMobileNumber { get; set; }
        public string Address { get; set; }
        //public string Company { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        //public double Balance { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }

    }
}