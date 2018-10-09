using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.ViewModels
{
    public class LocationModelView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
}