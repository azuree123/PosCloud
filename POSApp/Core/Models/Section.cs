using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Section : AuditableEntity
    {

       
        public int SectionId { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }


        public string Name { get; set; } // indian,turkish,

        public ICollection<POSTerminal> POSTerminals
        {
            get;
            set;
        }
        public ICollection<Product> Products
        {
            get;
            set;
        }

    }
}