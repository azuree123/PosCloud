using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class TimedEventEntityConfiguration : EntityTypeConfiguration<TimedEvent>
    {

        public TimedEventEntityConfiguration()
        {
            ToTable("TimedEvents", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Type).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Value).HasColumnType("float").IsRequired();
            Property(x => x.FromDate).HasColumnType("date").IsRequired();
            Property(x => x.ToDate).HasColumnType("date").IsRequired();
            Property(x => x.FromHour).HasColumnType("time(7)").IsRequired();
            Property(x => x.ToHour).HasColumnType("time(7)").IsRequired();
            Property(x => x.Days).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(x => x.IsActive).HasColumnType("bit").IsRequired();




            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.TimedEvents).HasForeignKey(x => x.StoreId).WillCascadeOnDelete(false);
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById}).WillCascadeOnDelete(false);
        }

    }
}