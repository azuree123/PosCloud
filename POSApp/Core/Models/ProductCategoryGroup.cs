using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using POSApp.Persistence;

namespace POSApp.Core.Models
{
    [Table("Clients", Schema = PosDbContext.DEFAULT_SCHEMA)]
    public class ProductCategoryGroup:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}