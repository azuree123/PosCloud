using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class AgentIncomeReportViewModel
    {
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public double Income { get; set; }
        public double Salary { get; set; }
        public double Commission { get; set; }
    }
}