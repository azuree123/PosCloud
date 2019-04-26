using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class BusinessPartnerViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Display(Name = "Branch", ResourceType = typeof(Resource))]
        public string BranchName { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered email format like (abc@example.com)")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string State { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public string City { get; set; }

        public DateTime Birthday { get; set; }
       
        public string Note { get; set; }
    }
}