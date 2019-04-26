using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class GenerateReportViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; } = DateTime.Today;
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; } = DateTime.Today;
    }
}