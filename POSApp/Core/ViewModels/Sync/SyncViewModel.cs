using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels.Sync
{
    public class SalesViewModel
    {
        public TransMaster SaleOrder { get; set; }
        public List<TransDetail> SaleOrderDetails { get; set; }

    }
    public class SyncObject
    {
        public string Object { get; set; }
    }
}