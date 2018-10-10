using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class PurchaseOrderDetailEntityConfiguration:EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailEntityConfiguration()
        {
            ToTable("PurchaseOrderDetails", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            Property(x => x.Quantity).HasColumnType("int").IsOptional();
            Property(x => x.RetailPrice).HasColumnType("float").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("float").IsOptional();

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            HasRequired(x=>x.Product).WithMany(x=>x.PurchaseOrderDetails).HasForeignKey(x=>x.ProductId).WillCascadeOnDelete(false);
            HasRequired(x=>x.PurchaseOrder).WithMany(x=>x.PurchaseOrderDetails).HasForeignKey(x=>x.PurchaseOrderId).WillCascadeOnDelete(true);
        }
    }
}