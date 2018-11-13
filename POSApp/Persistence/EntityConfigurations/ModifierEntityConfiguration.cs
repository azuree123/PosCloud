using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ModifierEntityConfiguration:EntityTypeConfiguration<Modifier>
    {
        public ModifierEntityConfiguration()
        {
            ToTable("Modifier", PosDbContext.DEFAULT_SCHEMA);

            HasKey(x => new {x.Id, x.StoreId});
            Property(x=>x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Barcode).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            HasRequired(x => x.Store).WithMany().HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            HasMany(x => x.Products).WithMany(x => x.Modifiers).Map(a =>
            {
                a.MapLeftKey("ProductCode", "ProductStoreCode");
                a.MapRightKey("ModifierId", "ModifierStoreId");
                a.ToTable("ProductModifiers", PosDbContext.DEFAULT_SCHEMA);
            });

        }
    }
}