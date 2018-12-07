using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TransMasterPaymentMethod:AuditableEntity
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public int TransMasterId { get; set; }
        public TransMaster TransMaster { get; set; }
    }
}