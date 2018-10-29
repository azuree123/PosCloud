using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class SaleOrderDetailEntityConfiguration : EntityTypeConfiguration<SaleOrderDetail>
    {

        public SaleOrderDetailEntityConfiguration()
        {
            ToTable("SaleOrderDetails", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Quantity).HasColumnType("int").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("decimal").IsOptional();
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();



            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.SaleOrder).WithMany(x => x.SaleOrderDetails).HasForeignKey(x => new {x.SaleOrderId,x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany().HasForeignKey(x => new {x.ProductId,x.StoreId }).WillCascadeOnDelete(false);
        }

    }
}