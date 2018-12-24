﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class ModifierLinkProductViewModel
    {
        public int ModifierId { get; set; }
        [DisplayName("Products")]
        public string ProductCode { get; set; }
        public int ProductStoreId { get; set; }
        public int ModifierStoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> ProductDDl { get; set; }
        public string ProductsDisplay { get; set; }
        public string[] Products { get; set; }
    }
}