using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace POSApp.Core.ViewModels
{
    public class TillOperationViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string ApplicationUserId { get; set; }
        public int? ShiftId { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Operation Date")]
        public DateTime OperationDate { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Opening Amount")]
        public decimal OpeningAmount { get; set; }
        [DisplayName("System Amount")]
        public decimal SystemAmount { get; set; }
        [DisplayName("Physical Amount")]
        public decimal PhysicalAmount { get; set; }
        public bool Status { get; set; }
        [DisplayName("TillOperation Type")]
        public string TillOperationType { get; set; }//Open or Close
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public decimal CarryOut { get; set; }

        public decimal AdjustedCashAmount { get; set; }
        public decimal AdjustedCreditAmount { get; set; }
        public decimal AdjustedCreditNoteAmount { get; set; }
        public IEnumerable<SelectListItem> ShiftDdl { get; set; }

    }
    public class TillOperationListModelView
    {
        public int? Id { get; set; }
        [DisplayName("Shift Name")]
        public string ShiftName { get; set; }
        [DisplayName("Operation Date")]
        public DateTime OperationDate { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Opening Amount")]
        public decimal OpeningAmount { get; set; }
        [DisplayName("System Amount")]
        public decimal SystemAmount { get; set; }
        public decimal PhysicalAmount { get; set; }
        public bool Status { get; set; }

        public decimal CarryOut { get; set; }

        public decimal AdjustedCashAmount { get; set; }
        public decimal AdjustedCreditAmount { get; set; }
        public decimal AdjustedCreditNoteAmount { get; set; }
        public string TillOperationType { get; set; }//Open or Close
        public int? StoreId { get; set; }
       
    }
}