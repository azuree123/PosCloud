using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class WeeklyTimeSalesViewModel
    {
        public decimal Amount { get; set; }
        public TimeSpan Time { get; set; }
    }
}