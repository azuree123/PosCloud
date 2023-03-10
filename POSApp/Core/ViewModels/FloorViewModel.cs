using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class FloorViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Floor Number")]
        public string FloorNumber { get; set; }
    }
}