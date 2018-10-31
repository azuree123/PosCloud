using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class TransMasterViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string TransCode { get; set; }
        public int? StoreId { get; set; }
        public int BusinessPartnerId { get; set; }
        public DateTime TransDate { get; set; }
        public string TransRef { get; set; }
        public string TransStatus { get; set; }
        public double TotalPrice { get; set; }
        public bool Posted { get; set; }
        public string ACRef { get; set; }
        public string PaymentMethod { get; set; }

        public IEnumerable<SelectListItem> SupplierDdl { get; set; }

    }
}