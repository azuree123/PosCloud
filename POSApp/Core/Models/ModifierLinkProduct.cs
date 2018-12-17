using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ModifierLinkProduct:AuditableEntity
    {
        public int ModifierId { get; set; }
        public Modifier Modifier { get; set; }
        public string ProductCode { get; set; }
        public Product Product { get; set; }
        public int ProductStoreId { get; set; }
        public int ModifierStoreId { get; set; }
        public Store ModifierStore { get; set; }
        public Store ProductStore { get; set; }

    }
}