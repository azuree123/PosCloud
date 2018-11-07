using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class GeneratePurchaseOrderViewModel
    {
        public TransMasterViewModel TransMasterViewModel { get; set; }
        public IEnumerable<TransDetailViewModel> TransDetailViewModels { get; set; }
        public CustomerModelView BusinessPartnerViewModel { get; set; }
        public decimal TotalAmount { get; set; }
    }
}