using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace POSApp.Core.Models
{
    public class Floor:AuditableEntity
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string FloorNumber { get; set; }
        public ICollection<DineTable> Tables { get; set; }
    }
}