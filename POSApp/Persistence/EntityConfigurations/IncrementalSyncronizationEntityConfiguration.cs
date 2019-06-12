using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class IncrementalSyncronizationEntityConfiguration : EntityTypeConfiguration<IncrementalSyncronization>
    {

        public IncrementalSyncronizationEntityConfiguration()
        {
            ToTable("IncrementalSyncronizations", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new { a.Id, a.StoreId,a.DeviceId });
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.LastSynced).HasColumnType("datetime").IsRequired();
            Property(a => a.TableName).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            HasRequired(x => x.Store).WithMany(a=>a.IncrementalSyncronizations).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(true);
            HasRequired(x => x.Device).WithMany(a =>a.IncrementalSyncronizations).HasForeignKey(x => new { x.DeviceId,x.StoreId }).WillCascadeOnDelete(true);

        }
    }
}