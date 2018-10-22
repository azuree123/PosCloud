using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels.Sync
{
    public class SyncViewModel
    {
        public SaleOrder SaleOrder { get; set; }
        public List<SaleOrderDetail> SaleOrderDetails { get; set; }

    }
}