using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class SaleOrderDetailEntityConfiguration : EntityTypeConfiguration<SaleOrderDetail>
    {

        public SaleOrderDetailEntityConfiguration()
        {
            ToTable("SaleOrderDetails", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Quantity).HasColumnType("int").IsOptional();
            Property(x => x.Price).HasColumnType("float").IsOptional();
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            


            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.SaleOrder).WithMany(x => x.SaleOrderDetails).HasForeignKey(x => x.SaleOrderId).WillCascadeOnDelete(true);
            HasRequired(x => x.Product).WithMany(x => x.SaleOrderDetails).HasForeignKey(x => x.ProductId).WillCascadeOnDelete(false);
        }

    }
}