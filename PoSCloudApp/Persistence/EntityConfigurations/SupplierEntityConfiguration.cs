using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class SupplierEntityConfiguration : EntityTypeConfiguration<Supplier>
    {

        public SupplierEntityConfiguration()
        {
            ToTable("Suppliers", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Address).HasColumnType("nvarchar").IsOptional();
           
            Property(x => x.State).HasColumnType("nvarchar").HasMaxLength(50).IsOptional();
            Property(x => x.City).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************



        }
    }
}