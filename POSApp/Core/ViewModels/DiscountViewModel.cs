using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class DiscountViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [DefaultValue(false)]
        [DisplayName("Is Percentage")]
        public bool IsPercentage { get; set; }
        public string Type { get; set; }
        public string Days { get; set; }
        public string[] tempDays { get; set; }
        [DisplayName("Discount Code")]
        public string DiscountCode { get; set; }
        public double Value { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid From")]
        public DateTime ValidFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid Till")]
        public DateTime ValidTill { get; set; }
        [DefaultValue(false)]
        [DisplayName("Is Taxable")]
        public bool IsTaxable { get; set; }
        public int? StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
    }
}