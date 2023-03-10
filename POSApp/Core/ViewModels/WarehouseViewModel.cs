using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class WarehouseViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        [DisplayName("Client")]
        public int ClientId { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> ClientDdl { get; set; }
    }
    public class WarehouseListModelView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
    }
}