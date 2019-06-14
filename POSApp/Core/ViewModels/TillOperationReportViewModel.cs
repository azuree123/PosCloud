using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class TillOperationReportViewModel
    {
        public string BranchName { get; set; }
        public string UserName { get; set; }
        public string ShiftName { get; set; }

        public DateTime OperationDate { get; set; }


        public decimal OpeningAmount { get; set; }

        public decimal SystemAmount { get; set; }

        public decimal ClosingAmount { get; set; }//physical amount/ carryforward
        public decimal Deficit { get; set; }//surplus and deficit
        public decimal CarryOut { get; set; }

        public decimal AdjustedCashAmount { get; set; }
        public decimal AdjustedCreditAmount { get; set; }
        public decimal AdjustedCreditNoteAmount { get; set; }
        public string TillOperationType { get; set; }//Open or Close


    }
}