using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class SecurityRightEntityConfiguration : EntityTypeConfiguration<SecurityRight>
    {
        public SecurityRightEntityConfiguration()
        {
            ToTable("SecurityRights", PosDbContext.DEFAULT_SCHEMA);// name table
            //******************************************************************************************* KEYS ********************
            //HasKey(x => x.Id);
            HasKey(x => new { x.IdentityUserRoleId, x.StoreId,x.SecurityObjectId });
           
            //******************************************************************************************* PROPERTIES ***************
           

            //************************************************************* ****************************** RELATIONS ****
            HasRequired(x => x.Store).WithMany(x => x.SecurityRights).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.SecurityObject).WithMany(x => x.SecurityRights).HasForeignKey(x => new { x.SecurityObjectId }).WillCascadeOnDelete(true);
            HasRequired(x => x.Role).WithMany(x => x.SecurityRights).HasForeignKey(x => new { x.IdentityUserRoleId }).WillCascadeOnDelete(true);

            //******************
            //HasMany(g => g.Operations).WithMany();
        }
    }
}
