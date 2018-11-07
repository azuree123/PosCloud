using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ModifierOption
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string CostType { get; set; }
        public double Price { get; set; }
        public int? TaxId { get; set; }
        public Tax Tax { get; set; }
        [DefaultValue(false)]
        public bool IsTaxable { get; set; }
        public int ModifierId { get; set; }
        public virtual Modifier Modifier { get; set; }
    }
}