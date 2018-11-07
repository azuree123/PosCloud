using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class DiscountEntityConfiguration : EntityTypeConfiguration<Discount>
    {
        public DiscountEntityConfiguration()
        {
            ToTable("Discounts", PosDbContext.DEFAULT_SCHEMA);// name table

            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.Value).HasColumnType("decimal").IsRequired();
            Property(x => x.Type).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.Days).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.IsPercentage).HasColumnType("bit").IsRequired();
            Property(x => x.IsTaxable).HasColumnType("bit").IsRequired();
            Property(x => x.IsActive).HasColumnType("bit").IsOptional();
            Property(x => x.ValidFrom).HasColumnType("datetime").IsOptional();
            Property(x => x.ValidTill).HasColumnType("datetime").IsOptional();
            Property(x => x.DiscountCode).HasColumnType("varchar").IsRequired().HasMaxLength(150);

            // *******************************************************************************************RELATIONS*****************
            HasRequired(x => x.Store).WithMany(x => x.Discounts).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

        }
    }
}
