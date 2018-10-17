using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class SaleOrderEntityConfiguration : EntityTypeConfiguration<SaleOrder>
    {

        public SaleOrderEntityConfiguration()
        {
            ToTable("SaleOrders", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Amount).HasColumnType("float").IsRequired();
            Property(x => x.Date).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            Property(x => x.Status).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(x => x.Tax).HasColumnType("float").IsOptional();
            Property(x => x.Time).HasColumnType("varchar").IsOptional();
            Property(x => x.Type).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Canceled).HasColumnType("bit").IsOptional();
            // Property(x => x.Synced).HasColumnType("bit").IsOptional();


            //******************************************************************************************* Auditable ***************

            //  Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            // Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.Customer).WithMany(x => x.SaleOrders).HasForeignKey(x => new {x.CustomerId, x.StoreId }).WillCascadeOnDelete(false);
        }

    }
}