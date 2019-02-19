using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class POSTerminal : AuditableEntity
    {
       
        //[ForeignKey("ApplicationUser")]
        public int POSTerminalId { get; set; }
        //[ForeignKey("ApplicationUser")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string ArabicName { get; set; }
        public int? SectionId { get; set; }
        public Section Section { get; set; }

        public bool IsActive { get; set; }

        // public ApplicationUser ApplicationUser { get; set; }


        public ICollection<ApplicationUser> ApplicationUsers { get; set; }


    }
}