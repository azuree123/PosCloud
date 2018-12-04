using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public int? StoreId { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered phone format like (waqar@gmail.com)")]
        public string Email { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        
    }
}