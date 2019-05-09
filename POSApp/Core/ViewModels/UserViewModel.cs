using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered phone format like (waqar@gmail.com)")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        public string RoleName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<RoleViewModel> RoleViewModels { get; set; }
    }
}