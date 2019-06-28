using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using POSApp.Resources;

namespace POSApp.Core.ViewModels
{
    public class ProductCreateViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]

        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]

        public string ArabicName { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]

        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]

        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [Display(Name = "size", ResourceType = typeof(Resource))]

        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("VAT")]


        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Resource))]

        public int CategoryId { get; set; }

        public string Duration { get; set; }
        
        public double UnitPrice { get; set; }
        [Display(Name = "CostPrice", ResourceType = typeof(Resource))]

        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public string Stock { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resource))]

        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Unit", ResourceType = typeof(Resource))]

        public int UnitId { get; set; }
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
       
        public IEnumerable<SelectListItem> CategoryDdl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }
        public IEnumerable<SelectListItem> TaxDdl { get; set; }
        public IEnumerable<SelectListItem> SectionDdl { get; set; }
        public IEnumerable<SelectListItem> SizeDdl { get; set; }
        public IEnumerable<SelectListItem> ModifierDDl { get; set; }
        public string ModifierDisplay { get; set; }
        public string[] Modifiers { get; set; }

        public List<ProductHelperViewModel> ProductHelperViewModels { get; set; } = new List<ProductHelperViewModel>();

    }
    public class ProductDtViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]

        public string Name { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]

        public string Description { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resource))]

        public string Type { get; set; }
        [Display(Name = "Productcode", ResourceType = typeof(Resource))]

        public string ProductCode { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]

        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [Display(Name = "size", ResourceType = typeof(Resource))]

        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("VAT")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public string Duration { get; set; }
        [Display(Name = "UnitPrice", ResourceType = typeof(Resource))]
        public double UnitPrice { get; set; }
        [Display(Name = "CostPrice", ResourceType = typeof(Resource))]
        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public double Stock { get; set; }
        public string Barcode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
        public int UnitId { get; set; }
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
        public IEnumerable<SelectListItem> CategoryDdl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }
        public IEnumerable<SelectListItem> TaxDdl { get; set; }
        public IEnumerable<SelectListItem> SectionDdl { get; set; }
        public IEnumerable<SelectListItem> SizeDdl { get; set; }


    }
    public class ProductSyncViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
      
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [DisplayName("Size")]
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("VAT")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
        public string Duration { get; set; }
       
        public double UnitPrice { get; set; }
        
        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public double Stock { get; set; }
        public string Barcode { get; set; }
      
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
        public int UnitId { get; set; }



    }
    public class ProductSyncWithImageViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }

        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [DisplayName("Size")]
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("VAT")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
        public string Duration { get; set; }

        public double UnitPrice { get; set; }

        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public double Stock { get; set; }
        public string Barcode { get; set; }

        public DateTime? CreatedOn { get; set; }
        public byte[] Image { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
        public int UnitId { get; set; }



    }

    public class ComboViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]
        public string ArabicName { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resource))]
        public string Type { get; set; }
        [Display(Name = "Productcode", ResourceType = typeof(Resource))]
        public string ProductCode { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [Display(Name = "size", ResourceType = typeof(Resource))]
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
        [DisplayName("VAT")]
        public int? TaxId { get; set; }
        [DisplayName("Is VAT Applicable?")]
        public bool IsTaxable { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resource))]
        public int CategoryId { get; set; }

        public string Duration { get; set; }
        [Display(Name = "UnitPrice", ResourceType = typeof(Resource))]
        public double UnitPrice { get; set; }
        [Display(Name = "CostPrice", ResourceType = typeof(Resource))]
        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public string Stock { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resource))]
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Unit", ResourceType = typeof(Resource))]
        public int UnitId { get; set; }
        public IEnumerable<SelectListItem> CategoryDdl { get; set; }
        public IEnumerable<SelectListItem> UnitDdl { get; set; }
        public IEnumerable<SelectListItem> TaxDdl { get; set; }
        public IEnumerable<SelectListItem> SectionDdl { get; set; }
        public IEnumerable<SelectListItem> SizeDdl { get; set; }
        public IEnumerable<ProductSubViewModel> ProductSubViewModels { get; set; }

    }

    public class ItemsViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Display(Name = "ArabicName", ResourceType = typeof(Resource))]
        public string ArabicName { get; set; }
        public int? StoreId { get; set; }
        [Display(Name = "Type", ResourceType = typeof(Resource))]
        public string Type { get; set; }
        [Display(Name = "Productcode", ResourceType = typeof(Resource))]
        public string ProductCode { get; set; }
        [Display(Name = "CostPrice", ResourceType = typeof(Resource))]
        public double CostPrice { get; set; }
        [Display(Name = "PurchaseUnit", ResourceType = typeof(Resource))]
        public string PurchaseUnit { get; set; }
        [Display(Name = "StorageUnit", ResourceType = typeof(Resource))]
        public string StorageUnit { get; set; }
        [Display(Name = "IngredientUnit", ResourceType = typeof(Resource))]
        public string IngredientUnit { get; set; }
        [Display(Name = "PurchasetoStorageFactor", ResourceType = typeof(Resource))]
        public decimal? PtoSFactor { get; set; }
        [DisplayName("Storage to Ingredient Factor")]
        public decimal? StoIFactor { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resource))]

        public string Barcode { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
        
        
        public IEnumerable<SelectListItem> TaxDdl { get; set; }
        
        public IEnumerable<SelectListItem> SizeDdl { get; set; }
        

    }

    public class ProductDdlViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resource))]

        public string Name { get; set; }
        [Display(Name = "UnitPrice", ResourceType = typeof(Resource))]
        public double UnitPrice { get; set; }
        [Display(Name = "CostPrice", ResourceType = typeof(Resource))]
        public double CostPrice { get; set; }

        [Display(Name = "PurchaseUnit", ResourceType = typeof(Resource))]
        public string PurchaseUnit { get; set; }
        [Display(Name = "StorageUnit", ResourceType = typeof(Resource))]
        public string StorageUnit { get; set; }
        [Display(Name = "IngredientUnit", ResourceType = typeof(Resource))]
        public string IngredientUnit { get; set; }

    }
}