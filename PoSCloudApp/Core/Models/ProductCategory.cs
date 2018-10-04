using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class ProductCategory:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Product> Products { get; set; }
       

    }
}