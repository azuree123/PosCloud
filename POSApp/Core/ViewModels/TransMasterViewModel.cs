using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class TransMasterViewModel
    {
        public int Id { get; set; }
        public string TransCode { get; set; }
        public int StoreId { get; set; }
        public int BusinessPartnerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransRef { get; set; }
        public string TransStatus { get; set; }
        public double TotalPrice { get; set; }
        public bool Posted { get; set; }
        public string ACRef { get; set; }
        public string PaymentMethod { get; set; }
    }
}