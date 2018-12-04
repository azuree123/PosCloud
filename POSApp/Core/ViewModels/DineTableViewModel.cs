using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class DineTableViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        [DisplayName("Table Number")]
        public string DineTableNumber { get; set; }
        public int FloorId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem>FloorDdl { get; set; }
    }
    public class DineTableListModelView
    {
        public int? Id { get; set; }
        [DisplayName("Display Name")]
        public string DineTableNumber { get; set; }
        public string FloorNumber { get; set; }
    }
}