using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class TillOperationViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string ApplicationUserId { get; set; }
        public int? ShiftId { get; set; }
        public DateTime OperationDate { get; set; }
        public string Remarks { get; set; }
        public decimal OpeningAmount { get; set; }
        public decimal SystemAmount { get; set; }
        public decimal PhysicalAmount { get; set; }
        public bool Status { get; set; }
        public string TillOperationType { get; set; }//Open or Close
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> ShiftDdl { get; set; }

    }
}