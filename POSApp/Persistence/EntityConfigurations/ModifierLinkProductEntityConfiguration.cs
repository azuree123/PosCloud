using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ModifierLinkProductEntityConfiguration:EntityTypeConfiguration<ModifierLinkProduct>
    {
        public ModifierLinkProductEntityConfiguration()
        {
            ToTable("ModifierLinkProducts", PosDbContext.DEFAULT_SCHEMA);
            HasKey(a => new {a.ModifierId, a.ProductCode});
            HasRequired(x => x.Product).WithMany(x => x.ModifierLinkProducts).HasForeignKey(s => new { s.ProductCode,s.ProductStoreId }).WillCascadeOnDelete(true);
            HasRequired(x => x.Modifier).WithMany(x => x.ModifierLinkProducts).HasForeignKey(s => new { s.ModifierId, s.ModifierStoreId }).WillCascadeOnDelete(true);

        }
    }
}