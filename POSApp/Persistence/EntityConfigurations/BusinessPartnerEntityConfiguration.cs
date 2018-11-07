using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    class BusinessPartnerEntityConfiguration : EntityTypeConfiguration<BusinessPartner>
    {
        public BusinessPartnerEntityConfiguration()
        {
            ToTable("BusinessPartners",PosDbContext.DEFAULT_SCHEMA);// name table
            //******************************************************************************************* KEYS ********************
            //HasKey(x => x.Id);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(150);
            Property(x => x.PhoneNumber).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Address).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Birthday).HasColumnType("datetime").IsOptional();
            Property(x => x.City).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.State).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Remarks).HasColumnType("varchar").HasMaxLength(250).IsOptional();
            Property(x => x.ContactPerson).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.CpMobileNumber).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Type).HasColumnType("char").HasMaxLength(1).IsOptional();
            Property(x => x.CreatedOn).IsOptional();
            Property(x => x.UpdatedOn).IsOptional();
            Property(x => x.CreatedById).IsOptional();
            Property(x => x.UpdatedById).IsOptional();

            //************************************************************* ****************************** RELATIONS ****
            HasRequired(x => x.Store).WithMany(x => x.BusinessPartners).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

            //******************
            //HasMany(g => g.Operations).WithMany();
        }
    }
}
