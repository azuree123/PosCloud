using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class SectionViewModel
    {
        public int? SectionId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; } // indian,turkish,
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}