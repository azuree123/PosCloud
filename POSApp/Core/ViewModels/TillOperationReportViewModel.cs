using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class TillOperationReportViewModel
    {

        public string BranchName { get; set; }
        public string UserEmail { get; set; }
        public string ShiftName { get; set; }
        
        public DateTime OperationDate { get; set; }
        
        
        public decimal OpeningAmount { get; set; }
      
        public decimal SystemAmount { get; set; }
       
        public decimal PhysicalAmount { get; set; }
        
       
        public string TillOperationType { get; set; }//Open or Close
   
        
    }
}