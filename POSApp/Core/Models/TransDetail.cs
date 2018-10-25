﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.Models
{
    public class TransDetail:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int TransMasterId { get; set; }
        public TransMaster TransMaster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }
        [DefaultValue(0)]
        public decimal UnitPrice { get; set; }
        [DefaultValue(0)]
        public decimal Discount { get; set; }
    }
}