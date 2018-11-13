using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {

        public ProductEntityConfiguration()
        {
            ToTable("Products", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.ProductCode, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Description).HasColumnType("varchar").HasMaxLength(300).IsOptional();
            Property(x => x.Barcode).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.ProductCode).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Image).HasColumnType("varbinary(MAX)").IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Stock).HasColumnType("float").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("float").IsRequired();
            Property(x => x.Attribute).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Size).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.CostPrice).HasColumnType("float").IsRequired();
            Property(x => x.ReOrderLevel).HasColumnType("int").IsRequired();
            Property(x => x.IsTaxable).HasColumnType("bit").IsRequired();
            Property(x => x.InventoryItem).HasColumnType("bit").IsRequired();
            Property(x => x.PurchaseItem).HasColumnType("bit").IsRequired();
            Property(x => x.FixedAssetItem).HasColumnType("bit").IsRequired();
            Property(x => x.IsTaxable).HasColumnType("bit").IsRequired();

            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => new {x.CategoryId,x.StoreId}).WillCascadeOnDelete(true);
            HasOptional(x => x.Tax).WithMany(x => x.Products).HasForeignKey(x => new {x.TaxId,x.StoreId}).WillCascadeOnDelete(false);
            HasRequired(x => x.ProductUnit).WithMany(x => x.Products).HasForeignKey(x => new { x.UnitId, x.StoreId }).WillCascadeOnDelete(false);
            HasOptional(x => x.Section).WithMany(x => x.Products).HasForeignKey(x => new { x.SectionId, x.StoreId }).WillCascadeOnDelete(false);
            //HasMany(x => x.TimedEvents).WithMany(x => x.Products).Map(a =>
            //{
            //    a.MapLeftKey("TimedEventId","TimedEventStoreId");
            //    a.MapRightKey("ProductId", "ProductStoreId");
            //    a.ToTable("TimedEventProducts", PosDbContext.DEFAULT_SCHEMA);
            //});

        }
    }
}