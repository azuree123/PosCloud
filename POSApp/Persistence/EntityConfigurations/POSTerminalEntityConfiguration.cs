using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class POSTerminalEntityConfiguration : EntityTypeConfiguration<POSTerminal>
    {
        public POSTerminalEntityConfiguration()
        {
            ToTable("POSTerminals", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new {a.POSTerminalId, a.StoreId});
            Property(a => a.POSTerminalId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(a => a.IsActive).HasColumnType("Bit").IsOptional();

            HasOptional(a => a.Section).WithMany(a => a.POSTerminals).HasForeignKey(x => new { x.SectionId, x.StoreId }).WillCascadeOnDelete(false);

        }
    }
}