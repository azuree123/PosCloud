using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class CityEntityConfiguration : EntityTypeConfiguration<City>
    {

        public CityEntityConfiguration()
        {
            ToTable("Cities", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.ArabicName).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            HasRequired(x => x.State).WithMany(x=>x.Cities).HasForeignKey(x=>x.StateId).WillCascadeOnDelete(false);
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById}).WillCascadeOnDelete(false);
        }

    }
}