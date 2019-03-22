using POSApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ComboProductTransDetailEntityConfiguration:EntityTypeConfiguration<ComboProductsTransDetail>

    {
    public ComboProductTransDetailEntityConfiguration()
    {
        ToTable("ComboProductTransDetails", PosDbContext.DEFAULT_SCHEMA);
        HasKey(a => new {a.Id, a.StoreId});
        Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        Property(a => a.Quantity).HasColumnType("int").IsRequired();
        Property(a => a.UnitPrice).HasColumnType("decimal").IsRequired();
        HasRequired(a => a.Store).WithMany(a => a.ComboProductsTransDetails).HasForeignKey(a => a.StoreId)
            .WillCascadeOnDelete(false);
        HasRequired(a => a.ProductsSub).WithMany(a => a.ComboProductsTransDetails)
            .HasForeignKey(a => new {a.ProductSubId, a.StoreId}).WillCascadeOnDelete(false);
        HasRequired(a => a.TransDetail).WithMany(a => a.ComboProductsTransDetails)
            .HasForeignKey(a => new {a.TransDetailId, a.StoreId}).WillCascadeOnDelete(false);

    }
    }
}