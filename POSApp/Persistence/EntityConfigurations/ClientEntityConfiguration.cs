using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ClientEntityConfiguration:EntityTypeConfiguration<Client>
    {
        public ClientEntityConfiguration()
        {
            ToTable("Clients", PosDbContext.DEFAULT_SCHEMA);

            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            
            Property(x => x.Address).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.Contact).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.City).HasColumnType("varchar").IsOptional().HasMaxLength(150);
            Property(x => x.State).HasColumnType("varchar").IsOptional().HasMaxLength(150);

            // *******************************************************************************************RELATIONS*****************
            

        }
    }
}