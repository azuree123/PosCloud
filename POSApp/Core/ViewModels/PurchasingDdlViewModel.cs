using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Core.ViewModels
{
    public class PurchasingDdlViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public string PurchaseUnit { get; set; }
        public decimal PurchaseQuantity { get; set; }
        public string StorageUnit { get; set; }
        public decimal StorageQuantity { get; set; }
        public string IngredientUnit { get; set; }
        public decimal IngredientQuantity { get; set; }

    }
}