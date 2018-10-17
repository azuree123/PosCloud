using System;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.ViewModels
{
    public class CustomerModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Referral { get; set; }
        //public string Gender { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}