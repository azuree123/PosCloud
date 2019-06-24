using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class AppCounterEntityConfiguration: EntityTypeConfiguration<AppCounter>
    {
        public AppCounterEntityConfiguration()
        {
            ToTable("AppCounters", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => new { x.Id, x.StoreId });

            HasRequired(c => c.Store)
                .WithMany(c => c.AppCounters).HasForeignKey(a => a.StoreId).WillCascadeOnDelete(true);
            HasOptional(c => c.Device)
                .WithMany(c => c.AppCounters).HasForeignKey(a => new {a.DeviceId,a.StoreId}).WillCascadeOnDelete(true);

        }
    }
}