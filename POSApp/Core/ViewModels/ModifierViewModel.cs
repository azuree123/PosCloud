using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ModifierViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resource))]
        public string Barcode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<ModifierOptionViewModel> ModifierOptionViewModels { get; set; }
    }
    public class ModifierListViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
       
        public string ArabicName { get; set; }
        public string Barcode { get; set; }
        public int ModifierOptions { get; set; }
        public int LinkedProducts { get; set; }

    }
}