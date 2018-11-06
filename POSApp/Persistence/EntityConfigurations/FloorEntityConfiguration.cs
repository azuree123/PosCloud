using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class FloorEntityConfiguration : EntityTypeConfiguration<Floor>
    {
        public FloorEntityConfiguration()
        {
            ToTable("Floors", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new {a.Id, a.StoreId});
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.FloorNumber).HasColumnType("varchar").HasMaxLength(10).IsRequired();

            
        }
    
}
}