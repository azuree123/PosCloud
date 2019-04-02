using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.ViewModels
{
    public class GeneratePurchaseOrderViewModel
    {
        public TransMasterViewModel TransMasterViewModel { get; set; }
        public IEnumerable<TransDetailViewModel> TransDetailViewModels { get; set; }
        public CustomerModelView BusinessPartnerViewModel { get; set; }
        public WarehouseViewModel WarehouseViewModel { get; set; }
        public TransMaster TransMaster { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitName { get; set; }
    }
}