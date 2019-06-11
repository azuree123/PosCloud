using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        
        public string Name { get; set; }

        public string TransferTo { get; set; }
        public int? WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int? BusinessPartnerId { get; set; }
        [DisplayName("Trans Date")]
        public string TransDate { get; set; }
        [DisplayName("Trans Time")]
        public string TransTime { get; set; }
        [DisplayName("Trans Ref")]
        public string TransRef { get; set; }
        [DisplayName("Trans Status")]
        public string TransStatus { get; set; }
        [DisplayName("Total Prices")]
        public double TotalPrice { get; set; }
        public bool Posted { get; set; }
        public string ACRef { get; set; }
        [DisplayName("Payment Method")]
        public string PaymentMethod { get; set; }
        [DisplayName("BusinessPartner Name")]
        public string BusinessPartnerName { get; set; }
        public bool Issued { get; set; }
        public string StoreName { get; set; }
        public string Code { get; set; }
        [DefaultValue(0)]
        public decimal Tax { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public IEnumerable<SelectListItem> SupplierDdl { get; set; }
        public IEnumerable<SelectListItem> WarehouseDdl { get; set; }
        public IEnumerable<SelectListItem> StoreDdl { get; set; }

        public IEnumerable<SelectListItem> PriDdl { get; set; }
        public int? DiscountId { get; set; }
        [DefaultValue(0)]
        public decimal Discount { get; set; }

        public List<TransDetailViewModel> TransDetailViewModels { get; set; } = new List<TransDetailViewModel>();
       
    }

  
  

}