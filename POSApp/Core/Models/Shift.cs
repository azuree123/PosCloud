using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Shift : AuditableEntity
    {
        public int ShiftId { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Name { get; set; }
        public string ArabicName { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<TillOperation> TillOperations { get; set; }

    }
}