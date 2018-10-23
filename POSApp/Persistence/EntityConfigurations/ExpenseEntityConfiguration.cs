using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ExpenseEntityConfiguration:EntityTypeConfiguration<Expense>
    {
        public ExpenseEntityConfiguration()
        {
            ToTable("Expenses", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Amount).HasColumnType("float").IsOptional();
            Property(x => x.Date).HasColumnType("datetime").IsOptional();
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            //Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);

            HasRequired(x=>x.ExpenseHead).WithMany(a=>a.Expenses).HasForeignKey(a=> new{a.ExpenseHeadId, a.StoreId }).WillCascadeOnDelete(false);
            HasRequired(x=>x.Employee).WithMany(x=>x.Expenses).HasForeignKey(x=> new {x.EmployeeId, x.StoreId }).WillCascadeOnDelete(false);
        }
    }
}