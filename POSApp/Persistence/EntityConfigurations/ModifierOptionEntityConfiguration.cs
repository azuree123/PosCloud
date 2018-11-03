using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ModifierOptionEntityConfiguration:EntityTypeConfiguration<ModifierOption>
    {
        public ModifierOptionEntityConfiguration()
        {
            ToTable("ModifierOptions", PosDbContext.DEFAULT_SCHEMA);

            HasKey(x => new {x.Id, x.StoreId});
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Cost).HasColumnType("float").IsRequired();
            Property(x => x.Price).HasColumnType("float").IsRequired();
            Property(x => x.CostType).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.IsTaxable).HasColumnType("bit").IsRequired();

            HasRequired(x=>x.Modifier).WithMany(x=>x.ModifierOptions).HasForeignKey(x=> new{x.ModifierId, x.StoreId}).WillCascadeOnDelete(true);
            HasRequired(x=>x.Tax).WithMany(x=>x.ModifierOptions).HasForeignKey(x=> new {x.TaxId, x.StoreId}).WillCascadeOnDelete(false);
        }
    }
}