using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class DeviceViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        
        public string ArabicName { get; set; }
        [Display(Name = "License", ResourceType = typeof(Resource))]
        public string License { get; set; }
        [DisplayName("Store")]
        public int StoreId { get; set; }
        
        [Display(Name = "DeviceCode", ResourceType = typeof(Resource))]
        public string DeviceCode { get; set; }
        [Display(Name = "AppVersion", ResourceType = typeof(Resource))]
        public string AppVersion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "DownloadedDate", ResourceType = typeof(Resource))]
        public DateTime DownloadedDate { get; set; } = DateTime.Today;
        [DisplayName("Receipt Header")]
        public string ReceiptHeader { get; set; }
        [DisplayName("Receipt Footer")]
        public string ReceiptFooter { get; set; }
       
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = "Must Enter Digits in password")]
        [DisplayName("Refund Pin")]
        [DataType(DataType.Password)]
        public string RefundPin { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string Contact { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public string City { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string State { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        
        public IEnumerable<SelectListItem> StoreDDl { get; set; }
    }
}