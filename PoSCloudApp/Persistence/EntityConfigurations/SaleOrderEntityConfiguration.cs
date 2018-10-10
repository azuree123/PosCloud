using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class SaleOrderEntityConfiguration : EntityTypeConfiguration<SaleOrder>
    {

        public SaleOrderEntityConfiguration()
        {
            ToTable("SaleOrders", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Code).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Amount).HasColumnType("float").IsOptional();
            Property(x => x.Date).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            Property(x => x.Status).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            Property(x => x.Tax).HasColumnType("float").IsOptional();
            Property(x => x.Time).HasColumnType("nvarchar").IsOptional();
            Property(x => x.Type).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Canceled).HasColumnType("bit").IsOptional();
            Property(x => x.Synced).HasColumnType("bit").IsOptional();


            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Customer).WithMany(x => x.SaleOrders).HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
        }

    }
}