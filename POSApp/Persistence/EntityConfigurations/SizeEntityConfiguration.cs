using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    
    
        public class SizeEntityConfiguration : EntityTypeConfiguration<Size>
        {
            public SizeEntityConfiguration()
            {
                ToTable("Sizes", PosDbContext.DEFAULT_SCHEMA);

                HasKey(a => new { a.Id, a.StoreId });
                Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                Property(a => a.Name).HasColumnType("varchar").HasMaxLength(100).IsRequired();


            }

        }
    
}