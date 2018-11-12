using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ApplicationRoleEntityConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleEntityConfiguration()
        {
            

            HasOptional(x => x.Store).WithMany().HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
        }
    }
}


