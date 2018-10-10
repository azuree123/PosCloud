using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class SaleOrderDetailEntityConfiguration : EntityTypeConfiguration<SaleOrderDetail>
    {

        public SaleOrderDetailEntityConfiguration()
        {
            ToTable("SaleOrderDetails", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Quantity).HasColumnType("int").IsOptional();
            Property(x => x.Price).HasColumnType("float").IsOptional();
            Property(x => x.Discount).HasColumnType("float").IsOptional();
            


            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(true);
            HasRequired(x => x.Designation).WithMany(x => x.Employees).HasForeignKey(x => x.DesignationId).WillCascadeOnDelete(true);
        }

    }
}