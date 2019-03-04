using System;
using System.ComponentModel;

namespace POSApp.Core.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
       
        public string ArabicName { get; set; }
        public byte[] Image { get; set; }
        [DisplayName("Group")]
        public string Type { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }

    }
    public class ProductCategoryDdlViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}