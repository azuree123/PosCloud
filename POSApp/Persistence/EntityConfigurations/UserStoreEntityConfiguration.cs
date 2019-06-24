using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class UserStoreEntityConfiguration:EntityTypeConfiguration<UserStore>
    {
        public UserStoreEntityConfiguration()
        {
            ToTable("UserStores", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => new { x.ApplicationUserId, x.StoreId });

            HasRequired(c => c.ApplicationUser)
.WithMany(c => c.UserStores).HasForeignKey(a=>a.ApplicationUserId).WillCascadeOnDelete(true);
            HasRequired(c => c.Store)
                .WithMany(c => c.UserStores).HasForeignKey(a => a.StoreId).WillCascadeOnDelete(true);

        }
    }
}