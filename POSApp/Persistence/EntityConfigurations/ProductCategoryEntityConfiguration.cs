using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ProductCategoryEntityConfiguration : EntityTypeConfiguration<ProductCategory>
    {

        public ProductCategoryEntityConfiguration()
        {
            ToTable("ProductCategories", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Image).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Type).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();



            //******************************************************************************************* Auditable ***************

            // Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            // Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);



        }
    }
}