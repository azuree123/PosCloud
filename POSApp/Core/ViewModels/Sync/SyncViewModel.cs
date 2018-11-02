using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels.Sync
{
    public class SalesViewModel
    {
        public TransMaster TransMaster { get; set; }
        public List<TransDetail> TransDetailsList { get; set; }

    }
    public class SyncObject
    {
        public string Object { get; set; }
    }
}