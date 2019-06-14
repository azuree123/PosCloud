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
           
            Property(x => x.PasswordEncrypt).HasColumnType("varchar").HasMaxLength(150).IsOptional(); 
           
            HasRequired(x => x.Store).WithMany().HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);
            HasOptional(x => x.POSTerminal).WithMany(a=>a.ApplicationUsers).HasForeignKey(x => new {x.POSTerminalId ,x.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x => x.Employee).WithMany(a => a.ApplicationUsers).HasForeignKey(x => new { x.EmployeeId, x.StoreId }).WillCascadeOnDelete(false);
            
        }
    }
}
