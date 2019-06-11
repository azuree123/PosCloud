using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class IncrementalSyncronizationViewModel
    {

        public int Id { get; set; }
        public DateTime LastSynced { get; set; }
        public string Licence { get; set; }

    }
}