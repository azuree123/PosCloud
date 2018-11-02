using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class ModifierOptionViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string CostType { get; set; }
        public double Price { get; set; }
        public int ModifierId { get; set; }
        public int? TaxId { get; set; }
        [DefaultValue(false)]
        public bool IsTaxable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> TaxDdl { get; set; }
    }
}