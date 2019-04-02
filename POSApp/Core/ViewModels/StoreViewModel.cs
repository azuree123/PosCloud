using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class StoreViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ClientId { get; set; }
        public string ArabicName { get; set; }

        public string Address { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format like (050xxxxxxxx)")]
        public string Contact { get; set; }

        public string City { get; set; }
        [DataType(DataType.Time)]
        public DateTime BusinessStartTime { get; set; }
        public string Currency { get; set; }
        public string State { get; set; }

        public bool IsOperational { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
    }
}