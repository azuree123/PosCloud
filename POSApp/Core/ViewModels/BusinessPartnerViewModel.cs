﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class BusinessPartnerViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
    }
}