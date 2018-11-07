using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public IEnumerable<SelectListItem>FloorDdl { get; set; }
    }
}