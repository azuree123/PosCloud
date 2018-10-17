using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class StateEntityConfiguration : EntityTypeConfiguration<State>
    {
        
            public StateEntityConfiguration()
            {
                ToTable("States", PosDbContext.DEFAULT_SCHEMA);
                //******************************************************************************************* KEYS ********************
                HasKey(x => x.Id);
                Property(x => x.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                //******************************************************************************************* PROPERTIES ***************
                Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();


            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById }).WillCascadeOnDelete(false);

        }

    }
}