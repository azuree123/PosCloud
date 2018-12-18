using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class ModifierViewModel
    {
        public int? Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
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
        public string Barcode { get; set; }
        public int ModifierOptions { get; set; }
        public int LinkedProducts { get; set; }

    }
}