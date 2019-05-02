using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ClientViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }


    }

    public class ClientApiViewModel{
        public int? Id { get; set; }
        public string Name { get; set; }
      
        public string Email { get; set; }
        public string ArabicName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Currency { get; set; }
        public bool IsOperational { get; set; }
        public DateTime BusinessStartTime { get; set; }
        public string VatNumber { get; set; }
    }
}