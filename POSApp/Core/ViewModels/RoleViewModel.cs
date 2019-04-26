using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        public int? StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<RoleSecurityRightViewModel> RoleSecurityRightViewModels { get; set; }
    }

    public class RoleSecurityRightViewModel
    {
        public int SecurityObjectId { get; set; }
        public string SecurityObject { get; set; }
        public string Module { get; set; }
        public bool Manage { get; set; }
        public bool View { get; set; }

    }
}