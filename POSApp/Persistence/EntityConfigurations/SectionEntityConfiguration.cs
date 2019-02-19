using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class SectionEntityConfiguration:EntityTypeConfiguration<Section>
    {
        public SectionEntityConfiguration()
        {
            ToTable("Sections", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new { a.SectionId, a.StoreId });
            Property(a => a.SectionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(a => a.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.ArabicName).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

        }

    }
}