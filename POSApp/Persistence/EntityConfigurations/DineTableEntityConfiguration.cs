using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class DineTableEntityConfiguration:EntityTypeConfiguration<DineTable>
    {
        public DineTableEntityConfiguration()
        {
            ToTable("DineTables", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => new {a.Id, a.StoreId});

            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

          // Property(a => a.DineTableNumber).HasColumnType("varchar").has  HasMaxLength(10).IsRequired();

            HasRequired(a => a.Floor).WithMany(a => a.Tables).HasForeignKey(a => new {a.FloorId, a.StoreId})
                .WillCascadeOnDelete(false);

        }
    }
}