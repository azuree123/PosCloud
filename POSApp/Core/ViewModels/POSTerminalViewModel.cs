using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class POSTerminalViewModel
    {
        public int? POSTerminalId { get; set; }
        //[ForeignKey("ApplicationUser")]
        public int StoreId { get; set; }
        public int? SectionId { get; set; }
        public string Name { get; set; }
        
        public string ArabicName { get; set; }
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