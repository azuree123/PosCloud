using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ClientViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Entered phone format like (03211234567)")]
        public string Contact { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}