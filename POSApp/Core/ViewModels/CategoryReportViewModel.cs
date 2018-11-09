﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class CategoryReportViewModel
    {
        public string CategoryName { get; set; }
        public double Qty { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}