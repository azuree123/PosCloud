﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ComboReportSaleViewModel
    {
        public string ComboName { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}