using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ProductCategoryGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}