using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TillOperation:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int SessionCode { get; set; }
        public Store Store { get; set; }

        public DateTime OperationDate { get; set; }

        public string Remarks { get; set; }

        public decimal OpeningAmount { get; set; }

        public decimal SystemAmount { get; set; }

        public decimal PhysicalAmount { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser Cashier { get; set; }

        public int? ShiftId { get; set; }

        public Shift Shift { get; set; }

        public bool Status { get; set; }

        public string TillOperationType { get; set; }//Open or Close
    }
}