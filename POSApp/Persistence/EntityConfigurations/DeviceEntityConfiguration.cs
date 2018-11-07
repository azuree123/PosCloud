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
            Property(x => x.License).HasColumnType("decimal").IsRequired();
            Property(x => x.DeviceCode).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.AppVersion).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.DownloadedDate).HasColumnType("datetime").IsOptional();
            Property(x => x.Address).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.Contact).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.City).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.State).HasColumnType("varchar").IsRequired().HasMaxLength(150);

            // *******************************************************************************************RELATIONS*****************
            HasRequired(x => x.Store).WithMany(x => x.Devices).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

        }
    }
}