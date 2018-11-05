using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class FloorViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        public int FloorNumber { get; set; }
    }
}