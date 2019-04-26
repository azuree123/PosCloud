using System;
using System.ComponentModel.DataAnnotations;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class StateModelView
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        public string ArabicName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
    }
}