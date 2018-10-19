using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace POSApp.Persistence.EntityConfigurations
{
    public class CouponEntityConfiguration : EntityTypeConfiguration<Coupon>
    {

        public CouponEntityConfiguration()
        {
            ToTable("Coupons", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Code).HasColumnType("varchar").IsOptional().HasMaxLength(4);
            Property(x => x.Value).HasColumnType("float").IsRequired();
            Property(x => x.ValidFrom).HasColumnType("date").IsRequired();
            Property(x => x.ValidTill).HasColumnType("date").IsRequired();
            Property(x => x.Amount).HasColumnType("int").IsRequired();
            Property(x => x.Days).HasColumnType("varchar").IsOptional().HasMaxLength(150);
            Property(x => x.IsPercentage).HasColumnType("bit").IsRequired();

            //******************************************************************************************* Auditable ***************


            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.Coupons).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
        }

    }
}