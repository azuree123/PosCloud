using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class CustomerEntityConfiguration : EntityTypeConfiguration<Customer>
    {

        public CustomerEntityConfiguration()
        {
            ToTable("Customers", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Address).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Birthday).HasColumnType("datetime").IsOptional();
            Property(x => x.City).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.State).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Gender).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Note).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Referral).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            //HasRequired(x => x.State).WithMany(x => x.Cities).HasForeignKey(x => x.StateId).WillCascadeOnDelete(true);
        }

    }
}