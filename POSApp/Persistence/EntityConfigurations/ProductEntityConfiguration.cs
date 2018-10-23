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
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Available).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Barcode).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Duration).HasColumnType("nvarchar").IsOptional();
            Property(x => x.Image).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Stock).HasColumnType("float").IsOptional();
            Property(x => x.Tax).HasColumnType("float").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("float").IsOptional();

            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => new {x.CategoryId,x.StoreId}).WillCascadeOnDelete(true);
            

        }
    }
}