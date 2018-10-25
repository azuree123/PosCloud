﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class ProductCategoryGroup:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}