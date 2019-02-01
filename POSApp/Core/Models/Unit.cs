using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class Unit:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public string UnitCode { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}