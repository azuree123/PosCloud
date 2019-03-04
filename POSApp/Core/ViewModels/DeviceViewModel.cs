using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class DeviceViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        
        public string ArabicName { get; set; }
        public string License { get; set; }
        [DisplayName("Store")]
        public int StoreId { get; set; }
        [DisplayName("Device Code")]
        public string DeviceCode { get; set; }
        [DisplayName("App Version")]
        public string AppVersion { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Downloaded Date")]
        public DateTime DownloadedDate { get; set; }
        [DisplayName("Receipt Header")]
        public string ReceiptHeader { get; set; }
        [DisplayName("Receipt Footer")]
        public string ReceiptFooter { get; set; }
       
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = "Must Enter Digits in password")]
        [DisplayName("Refund Pin")]
        [DataType(DataType.Password)]
        public string RefundPin { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        
        public IEnumerable<SelectListItem> StoreDDl { get; set; }
    }
}