using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TransMasterPaymentMethodViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public int TransMasterId { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}