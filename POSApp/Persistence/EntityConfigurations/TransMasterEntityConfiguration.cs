using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    class TransMasterEntityConfiguration : EntityTypeConfiguration<TransMaster>
    {
        public TransMasterEntityConfiguration()
        {
            ToTable("TransMaster",PosDbContext.DEFAULT_SCHEMA);// name table

            //******************************************************************************************* KEYS ********************
            //HasKey(x => x.Id);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.ACRef).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(x => x.TransCode).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(x => x.TransRef).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(x => x.TransStatus).HasColumnType("varchar").HasMaxLength(25).IsOptional();

            Property(x => x.Type).HasColumnType("char").HasMaxLength(3).IsOptional();
            Property(x => x.CreatedOn).IsOptional();
            Property(x => x.UpdatedOn).IsOptional();
            Property(x => x.CreatedById).IsOptional();
            Property(x => x.UpdatedById).IsOptional();

            HasIndex(p => new { p.TransCode }).IsUnique();

            HasRequired(x => x.Store).WithMany(x => x.TransMasters).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.BusinessPartner).WithMany(x => x.TransMasters).HasForeignKey(x => new { x.BusinessPartnerId, x.StoreId }).WillCascadeOnDelete(false);
            HasOptional(x => x.DineTable).WithMany(x => x.TransMasters).HasForeignKey(x => new { x.TableId, x.StoreId }).WillCascadeOnDelete(false);


        }
    }
}