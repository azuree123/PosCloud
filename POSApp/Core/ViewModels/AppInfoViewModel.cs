using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class AppInfoViewModel
    {
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public DateTime BusinessStartTime { get; set; }
        public string DeviceName { get; set; }
        public string ReceiptHeader { get; set; }
        public string ReceiptFooter { get; set; }
        public string Currency { get; set; }
        public string ExchangePin { get; set; }
        public string RefundPin { get; set; }
        public int DeviceId { get; set; }
        public string StoreAddress { get; set; }
        public int StoreId { get; set; }
    }
}