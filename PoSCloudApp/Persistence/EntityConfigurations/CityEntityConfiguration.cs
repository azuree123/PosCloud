using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models.DbModels;

namespace PoSCloudApp.Persistence.EntityConfigurations
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

            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************
            
            HasRequired(x => x.State).WithMany(x=>x.Cities).HasForeignKey(x=>x.StateId).WillCascadeOnDelete(true);
        }

    }
}