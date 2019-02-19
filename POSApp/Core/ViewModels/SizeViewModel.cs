using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class SizeViewModel
    {
        public int? Id { get; set; }
        public int StoreId { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        public string ArabicName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
       
    }
}