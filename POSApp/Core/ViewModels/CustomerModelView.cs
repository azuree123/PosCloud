using System;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.ViewModels
{
    public class CustomerModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArabicName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Entered phone format like (abc@example.com)")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Entered phone format like (050xxxxxxx)")]
        public string PhoneNumber { get; set; }
        //public string Referral { get; set; }
        //public string Gender { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}