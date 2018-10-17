using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class EmployeeEntityConfiguration : EntityTypeConfiguration<Employee>
    {

        public EmployeeEntityConfiguration()
        {
            ToTable("Employees", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Address).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            //Property(x => x.Booking).HasColumnType("bit").IsOptional();
            Property(x => x.Commission).HasColumnType("float").IsOptional();
            Property(x => x.Gender).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.JoinDate).HasColumnType("datetime").IsOptional();
            Property(x => x.MobileNumber).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Salary).HasColumnType("float").IsOptional();
           

            //******************************************************************************************* Auditable ***************

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            //******************************************************************************************* Auditable ***************

            HasRequired(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => new{x.DepartmentId}).WillCascadeOnDelete(false);
            HasRequired(x => x.Designation).WithMany(x => x.Employees).HasForeignKey(x => new{x.DesignationId}).WillCascadeOnDelete(false);
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);
        }

    }
}