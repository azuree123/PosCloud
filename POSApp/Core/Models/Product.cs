using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace POSApp.Core.Models
{
    public class Product:AuditableEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }//Sale item,Service
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        public int? TaxId { get; set; }
        public Tax Tax { get; set; }
        [DefaultValue(false)]
        public bool IsTaxable { get; set; }
        [DefaultValue(0)]
        public double UnitPrice { get; set; }
        [DefaultValue(0)]
        public double CostPrice { get; set; }
        [DefaultValue(0)]
        public double Stock { get; set; }
        [DefaultValue(0)]
        public int ReOrderLevel { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int UnitId { get; set; }
        public Unit ProductUnit { get; set; }
        public int? SectionId { get; set; }
        public Section Section { get; set; }
        
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }

        public virtual ICollection<TransDetail> TransDetails { get; set; }

        public virtual ICollection<Modifier> Modifiers { get; set; }
        public virtual ICollection<ProductsSub> ProductsSubs { get; set; }
        public virtual ICollection<ProductsSub> ComboProducts { get; set; }

        public virtual ICollection<TimedEventProducts> TimedEventProducts { get; set; }
        public ICollection<ModifierLinkProduct> ModifierLinkProducts { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<Recipe> IngredientRecipes { get; set; }


    }
}