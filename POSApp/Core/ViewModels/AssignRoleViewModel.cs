using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class AssignRoleViewModel
    {
        [Required(ErrorMessage = "Roles")]
        public string RoleName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Users")]
        public string UserName
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> Userlist
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> UserRolesList
        {
            get;
            set;
        }
    }
}