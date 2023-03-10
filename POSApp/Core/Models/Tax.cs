using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Tax: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        [DefaultValue(0)]
        public double Rate { get; set; }
        [DefaultValue(0)]
        public bool? IsPercentage { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ModifierOption> ModifierOptions { get; set; }
    }
}