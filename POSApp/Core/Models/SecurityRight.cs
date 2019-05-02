
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace POSApp.Core.Models
{
    public class SecurityRight
    {
        public string IdentityUserRoleId { get; set; }       
        public ApplicationRole Role { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int SecurityObjectId { get; set; }
        public SecurityObject SecurityObject { get; set; }
        [DefaultValue(false)]
        public bool Manage { get; set; }
        [DefaultValue(true)]
        public bool View { get; set; }


    }
}
