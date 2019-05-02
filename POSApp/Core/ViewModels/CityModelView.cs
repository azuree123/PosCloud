using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class CityModelView
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]
        public string ArabicName { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resource))]
        public int StateId { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> StateDdl { get; set; }
    }
    public class CityListModelView
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        public string StateName { get; set; }
    }
}