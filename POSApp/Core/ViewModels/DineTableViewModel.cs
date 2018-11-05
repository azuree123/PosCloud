using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class DineTableViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        public int DineTableNumber { get; set; }
        public int FloorId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}