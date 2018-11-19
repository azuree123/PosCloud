﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSApp.Core.ViewModels
{
    public class SupplierModelView
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("CP Mobile Number")]
        public string CpMobileNumber { get; set; }
        public string Address { get; set; }
        //public string Company { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        //public double Balance { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }

    }
}