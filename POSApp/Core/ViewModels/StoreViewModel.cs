using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class StoreViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "Clients", ResourceType = typeof(Resource))]

        public int? ClientId { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format like (050xxxxxxxx)")]
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public string Contact { get; set; }
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public string City { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "BussinessStartTime", ResourceType = typeof(Resource))]

        public DateTime BusinessStartTime { get; set; } = DateTime.Today;
        [Display(Name = "Currency", ResourceType = typeof(Resource))]

        public string Currency { get; set; }
        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string State { get; set; }
        [Display(Name = "IsOperational", ResourceType = typeof(Resource))]
        public bool IsOperational { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        public string Code { get; set; }
    }
}