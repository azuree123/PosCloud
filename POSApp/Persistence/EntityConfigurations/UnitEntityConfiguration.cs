using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class UnitEntityConfiguration : EntityTypeConfiguration<Unit>
    {

        public UnitEntityConfiguration()
        {
            ToTable("Units", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new{x.Id,x.StoreId});
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.ArabicName).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.UnitCode).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************


            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.Units).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

        }

    }
}