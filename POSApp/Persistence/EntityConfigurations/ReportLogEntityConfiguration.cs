using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ReportLogEntityConfiguration : EntityTypeConfiguration<ReportsLog>
    {

        public ReportLogEntityConfiguration()
        {
            ToTable("ReportLogs", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new {x.Id,x.StoreId});
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Details).HasColumnType("varchar").IsOptional();
            Property(x => x.Path).HasColumnType("varchar").IsRequired();
            Property(x => x.Status).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************


            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.ReportsLogs).HasForeignKey(x => x.StoreId).WillCascadeOnDelete(false);

        }

    }
}