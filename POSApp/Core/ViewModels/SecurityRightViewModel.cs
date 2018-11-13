using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class SecurityRightViewModel
    {
        public string IdentityUserRoleId { get; set; }
        public int StoreId { get; set; }
        public int SecurityObjectId { get; set; }
        public bool Manage { get; set; }
        public bool View { get; set; }
    }
}