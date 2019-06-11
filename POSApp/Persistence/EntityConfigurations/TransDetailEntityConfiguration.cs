using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    class TransDetailEntityConfiguration : EntityTypeConfiguration<TransDetail>
    {
        public TransDetailEntityConfiguration()
        {
            ToTable("TransDetails",PosDbContext.DEFAULT_SCHEMA);// name table

            //******************************************************************************************* KEYS ********************
            //HasKey(x => x.Id);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Tax).HasColumnType("decimal").IsRequired();
            Property(x => x.Quantity).HasColumnType("decimal").IsRequired();
            Property(x => x.Balance).HasColumnType("decimal").IsRequired();
            Property(x => x.Discount).HasColumnType("decimal").IsOptional();
            Property(x => x.BatchNumber).HasColumnType("nvarchar").IsOptional();
            Property(x => x.ManufactureDate).HasColumnType("datetime").IsOptional();
            Property(x => x.ExpiryDate).HasColumnType("datetime").IsOptional();
            Property(x => x.Waste).HasColumnType("bit").IsOptional();
            Property(x => x.CreatedOn).IsOptional();
            Property(x => x.UpdatedOn).IsOptional();
            Property(x => x.CreatedById).IsOptional();
            Property(x => x.UpdatedById).IsOptional();
            // *******************************************************************************************RELATIONS*****************

            HasRequired(x => x.Store).WithMany().HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

            HasRequired(x => x.TransMaster).WithMany(x => x.TransDetails).HasForeignKey(x => new { x.TransMasterId, x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany(x => x.TransDetails).HasForeignKey(x => new { x.ProductCode, x.StoreId }).WillCascadeOnDelete(true);
            HasOptional(x => x.TimedEvent).WithMany(x => x.TransDetails).HasForeignKey(x => new { x.DiscountId, x.StoreId }).WillCascadeOnDelete(true);

        }
    }
}