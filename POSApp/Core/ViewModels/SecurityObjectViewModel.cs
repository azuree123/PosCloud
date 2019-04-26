using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class SecurityObjectViewModel
    {
        public int? SecurityObjectId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }// varchar 150
        [Display(Name = "Type", ResourceType = typeof(Resource))]
        public string Type { get; set; }// Form, report etc // varchar 15
        [Display(Name = "Module", ResourceType = typeof(Resource))]
        public string Module { get; set; }//HR, Finane etc 50
    }
}