﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.Models
{
    public class SaleOrderDetails:AuditableEntity
    {
        public int Id { get; set; }
        public int SaleOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}