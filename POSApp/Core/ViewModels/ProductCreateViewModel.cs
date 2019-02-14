using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace POSApp.Core.ViewModels
{
    public class ProductCreateViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("Tax")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public string Duration { get; set; }
        
        public double UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public string Stock { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
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
    public class ProductDtViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("Tax")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

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
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        [DisplayName("Size")]
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("Tax")]
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
        public string Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
        public int UnitId { get; set; }



    }

    public class ComboViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public string Attribute { get; set; }//Black, Grey, Tan or Right, Left, Tikka or Fajeeta etc
        public string Size { get; set; }//MED, SML, LRG,XRG, Child,
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        public bool InventoryItem { get; set; }
        public bool PurchaseItem { get; set; }
        public bool FixedAssetItem { get; set; }
        [DisplayName("Tax")]
        public int? TaxId { get; set; }
        public bool IsTaxable { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public string Duration { get; set; }
        public double UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public int ReOrderLevel { get; set; }

        public string Stock { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayName("Unit")]
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
        public string Name { get; set; }
        public int? StoreId { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public double CostPrice { get; set; }
        public string PurchaseUnit { get; set; }
        public string StorageUnit { get; set; }
        public string IngredientUnit { get; set; }
        [DisplayName("Purchase to Storage Factor")]
        public decimal? PtoSFactor { get; set; }
        [DisplayName("Storage to Ingredient Factor")]
        public decimal? StoIFactor { get; set; }
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
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double CostPrice { get; set; }
        public string PurchaseUnit { get; set; }
        public string StorageUnit { get; set; }
        public string IngredientUnit { get; set; }

    }
}