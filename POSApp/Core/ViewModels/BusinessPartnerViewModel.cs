using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class BusinessPartnerViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }

        public string Name { get; set; }

        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Entered phone format like (03211234567)")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered email format like (waqar@gmail.com)")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
    }
}