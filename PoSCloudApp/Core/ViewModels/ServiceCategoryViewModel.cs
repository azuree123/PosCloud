using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoSCloudApp.Core.ViewModels
{
    public class ServiceCategoryViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}