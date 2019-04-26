using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class UnitViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        public string ArabicName { get; set; }

        [DisplayName("Unit Code")]
        public string UnitCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}