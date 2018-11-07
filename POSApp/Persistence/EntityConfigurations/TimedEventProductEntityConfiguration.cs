using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class TimedEventProductEntityConfiguration : EntityTypeConfiguration<TimedEventProducts>
    {

        public TimedEventProductEntityConfiguration()
        {
            ToTable("TimedEventProducts", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new{x.ProductId, x.StoreId,x.TimedEventId});

            //******************************************************************************************* PROPERTIES ***************
           

            //******************************************************************************************* Auditable ***************

            //******************************************************************************************* Auditable ***************
            HasRequired(x => x.Store).WithMany(x => x.TimedEventProducts).HasForeignKey(s => new { s.StoreId}).WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany(x => x.TimedEventProducts).HasForeignKey(x => new{x.ProductId,x.StoreId }).WillCascadeOnDelete(true);
            HasRequired(x => x.TimedEvent).WithMany(x => x.TimedEventProducts).HasForeignKey(x => new { x.TimedEventId ,x.StoreId}).WillCascadeOnDelete(true);

            //modelBuilder.Entity<Contract>()
            //    .HasMany(c => c.ContractParts)
            //    .WithRequired()
            //    .HasForeignKey(cp => cp.ContractId);

            //modelBuilder.Entity<Part>()
            //    .HasMany(p => p.ContractParts)
            //    .WithRequired()
            //    .HasForeignKey(cp => cp.PartId);
        }

    }
}