using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class PurchaseOrderDetailEntityConfiguration:EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailEntityConfiguration()
        {
            ToTable("PurchaseOrderDetails", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            Property(x => x.Quantity).HasColumnType("int").IsOptional();
            //Property(x => x.RetailPrice).HasColumnType("float").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("decimal").IsOptional();

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x=>x.Product).WithMany(x=>x.PurchaseOrderDetails).HasForeignKey(x=> new {x.ProductId,x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x=>x.PurchaseOrder).WithMany(x=>x.PurchaseOrderDetails).HasForeignKey(x=> new {x.PurchaseOrderId,x.StoreId }).WillCascadeOnDelete(false);
        }
    }
}