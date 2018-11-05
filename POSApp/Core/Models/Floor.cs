using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace POSApp.Core.Models
{
    public class Floor
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public ICollection<DineTable> Tables { get; set; }
    }
}