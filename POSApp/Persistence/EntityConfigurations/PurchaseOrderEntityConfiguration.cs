using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class PurchaseOrderEntityConfiguration:EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderEntityConfiguration()
        {
            ToTable("PurchaseOrders", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.OrderDate).HasColumnType("datetime").IsOptional();
            Property(x => x.SupplyDate).HasColumnType("datetime").IsOptional();
            Property(x => x.TotalPrice).HasColumnType("float").IsOptional();
            Property(x => x.Type).HasColumnType("varchar").IsOptional();
            //******************************************************************************************* Auditable ***************
            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x=>x.Supplier).WithMany(x=>x.PurchaseOrders).HasForeignKey(x=>new {x.SupplierId, x.StoreId }).WillCascadeOnDelete(false);
        }
    }
}