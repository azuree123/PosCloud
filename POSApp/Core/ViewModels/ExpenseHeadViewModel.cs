using System;
using System.ComponentModel.DataAnnotations;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ExpenseHeadViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        
        public string ArabicName { get; set; }
        [Display(Name = "Details", ResourceType = typeof(Resource))]
        public string Details { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
    }
}