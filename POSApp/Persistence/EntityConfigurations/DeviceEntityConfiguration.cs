using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class DeviceEntityConfiguration : EntityTypeConfiguration<Device>
    {
        public DeviceEntityConfiguration()
        {
            ToTable("Devices", PosDbContext.DEFAULT_SCHEMA);// name table

            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.License).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.DeviceCode).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.AppVersion).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.DownloadedDate).HasColumnType("datetime").IsOptional();
            Property(x => x.Address).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.Contact).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.City).HasColumnType("varchar").IsOptional().HasMaxLength(150);
            Property(x => x.State).HasColumnType("varchar").IsOptional().HasMaxLength(150);
            Property(x => x.ArabicName).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.ReceiptHeader).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.ReceiptFooter).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.RefundPin).HasColumnType("varchar").IsRequired();

            // *******************************************************************************************RELATIONS*****************
            HasRequired(x => x.Store).WithMany(x => x.Devices).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            
        }
    }
}