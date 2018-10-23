using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ApplicationUserEntityConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserEntityConfiguration()
        {
            //ToTable("Cities", PosDbContext.DEFAULT_SCHEMA);
            // ToTable("AspNetUsers");
            //******************************************************************************************* KEYS ********************
            // HasKey(x => new { x.Id, x.StoreId });
            //Property(x => x.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            // Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();;
            HasOptional(x => x.Employee).WithMany().HasForeignKey(x => new {x.EmployeeId}).WillCascadeOnDelete(false);

            HasOptional(x => x.Store).WithMany().HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById}).WillCascadeOnDelete(true);
        }
    }
}
//    public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
//    {
//        public UserClaimConfiguration()
//        {
//            ToTable("AspNetUserClaims");
//            //******************************************************************************************* KEYS ********************
//            HasKey(x => new { x.Id, x.StoreId });
//            Property(x => x.Id)
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//            HasOptional(x => x.UserId).WithMany().HasForeignKey(x => new { x.UserId, x.StoreId }).WillCascadeOnDelete(false);
//        }

//    }
//    public class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
//    {
//        public UserLoginConfiguration()
//        {
//            ToTable("AspNetUserLogins");
//            //******************************************************************************************* KEYS ********************
//            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId, x.StoreId });
//            //Property(x => x.Id)
//            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//        }

//    }
//    public class RoleConfiguration : EntityTypeConfiguration<Role>
//    {
//        public RoleConfiguration()
//        {
//            ToTable("AspNetRoles");
//            //******************************************************************************************* KEYS ********************
//            HasKey(x => new { x.Id, x.StoreId });
//            //Property(x => x.Id)
//            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//        }

//    }
//    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
//    {
//        public UserRoleConfiguration()
//        {
//            ToTable("AspNetUserRoles");
//            //******************************************************************************************* KEYS ********************
//            HasKey(x => new { x.UserId, x.RoleId, x.StoreId });
//            //Property(x => x.Id)
//            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//        }

//    }
//}