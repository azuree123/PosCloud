using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class PurchaseOrderEntityConfiguration:EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderEntityConfiguration()
        {
            ToTable("PurchaseOrders", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.OrderDate).HasColumnType("datetime").IsOptional();
            Property(x => x.SupplyDate).HasColumnType("datetime").IsOptional();
            Property(x => x.TotalPrice).HasColumnType("float").IsOptional();
            Property(x => x.Type).HasColumnType("nvarchar").IsOptional();
            //******************************************************************************************* Auditable ***************
            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            HasRequired(x=>x.Supplier).WithMany(x=>x.PurchaseOrders).HasForeignKey(x=>x.SupplierId).WillCascadeOnDelete(true);
        }
    }
}