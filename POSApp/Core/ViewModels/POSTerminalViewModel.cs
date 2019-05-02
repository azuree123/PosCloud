using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class POSTerminalViewModel
    {
        public int? POSTerminalId { get; set; }
        //[ForeignKey("ApplicationUser")]
        public int StoreId { get; set; }
        public int? SectionId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Resource))]
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> SectionDdl { get; set; }

    }
    public class POSTerminalListModelView
    {
        public int? POSTerminalId { get; set; }
        public string POSTerminalName { get; set; }
        public string Section { get; set; }
    }
}