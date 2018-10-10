using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {

        public ProductEntityConfiguration()
        {
            ToTable("Products", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Available).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Barcode).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Duration).HasColumnType("nvarchar").IsOptional();
            Property(x => x.Image).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.ProductCode).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Stock).HasColumnType("float").IsOptional();
            Property(x => x.Tax).HasColumnType("float").IsOptional();
            Property(x => x.UnitPrice).HasColumnType("float").IsOptional();


            //******************************************************************************************* Auditable ***************

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.ProductCategory).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
            HasRequired(x => x.Supplier).WithMany(x => x.Products).HasForeignKey(x => x.SupplierId)
                .WillCascadeOnDelete(false);


        }
    }
}