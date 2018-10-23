using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class CityModelView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [DisplayName("State")]
        public int StateId { get; set; }
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