using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class SecurityObjectViewModel
    {
        public int? SecurityObjectId { get; set; }
        public string Name { get; set; }// varchar 150
        public string Type { get; set; }// Form, report etc // varchar 15
        public string Module { get; set; }//HR, Finane etc 50
    }
}