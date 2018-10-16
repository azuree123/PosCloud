﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime SupplyDate { get; set; }
        public int InvoiceId { get; set; }
        [DefaultValue(0)]
        public double TotalPrice { get; set; }
        public string Type { get; set; }
    }
}