using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ModifierLinkProductViewModel
    {
        public int ModifierId { get; set; }
        public string ProductCode { get; set; }
        public int ProductStoreId { get; set; }
        public int ModifierStoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}