using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class  ProductsSubEntityConfiguration : EntityTypeConfiguration<ProductsSub>
    {

        public   ProductsSubEntityConfiguration()
        {
            ToTable("ProductsSubs", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x =>new{ x.ComboProductId,x.StoreId,x.ProductId});
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Qty).HasColumnType("float").IsRequired();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

           

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Store).WithMany(x => x.ProductsSubs).HasForeignKey(x => x.StoreId).WillCascadeOnDelete(false);
            HasRequired(x => x.ComboProduct).WithMany(x => x.ComboProducts).HasForeignKey(x => new{x.ComboProductId,x.StoreId}).WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany(x => x.ProductsSubs).HasForeignKey(x => new { x.ProductId,x.StoreId }).WillCascadeOnDelete(false);


        }

    }
}