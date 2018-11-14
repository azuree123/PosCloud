using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ShiftEntityConfiguration:EntityTypeConfiguration<Shift>
    {
        public ShiftEntityConfiguration()
        {
            ToTable("Shifts", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new { a.ShiftId, a.StoreId });
            Property(a => a.ShiftId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(a => a.Name).HasColumnType("varchar").HasMaxLength(10).IsRequired();
        }
    }
}