using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ModifierTransDetailEntityConfiguration:EntityTypeConfiguration<ModifierTransDetail>
    {
        public ModifierTransDetailEntityConfiguration()
        {
            ToTable("ModifierTransDetails", PosDbContext.DEFAULT_SCHEMA);
            HasKey(a => new {a.Id, a.StoreId});
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(a => a.Quantity).HasColumnType("int").IsRequired();
            Property(a => a.UnitPrice).HasColumnType("decimal").IsRequired();
            HasRequired(a=>a.Store).WithMany(a=>a.ModifierTransDetails).HasForeignKey(a=>a.StoreId).WillCascadeOnDelete(false);
            HasRequired(a=>a.ModifierOption).WithMany(a=>a.ModifierTransDetail).HasForeignKey(a=> new{a.ModifierOptionId,a.StoreId}).WillCascadeOnDelete(false);
            HasRequired(a=>a.TransDetail).WithMany(a=>a.ModifierTransDetail).HasForeignKey(a=> new{a.TransDetailId,a.StoreId}).WillCascadeOnDelete(false);

        }
    }
}