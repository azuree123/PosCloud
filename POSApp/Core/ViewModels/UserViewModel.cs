﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        
    }
}