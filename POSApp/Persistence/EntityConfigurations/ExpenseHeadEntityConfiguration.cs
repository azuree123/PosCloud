using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class ExpenseHeadEntityConfiguration:EntityTypeConfiguration<ExpenseHead>
    {
        public ExpenseHeadEntityConfiguration()
        {
            ToTable("ExpenseHeads", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.ArabicName).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            Property(x => x.Details).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();

            //Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            // Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            //HasRequired(x => x.CreatedBy).WithMany().HasForeignKey(x => new { x.CreatedById, x.StoreId }).WillCascadeOnDelete(false);
            //HasRequired(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);

            HasRequired(x => x.Store).WithMany(x => x.ExpenseHeads).HasForeignKey(x => new { x.StoreId }).WillCascadeOnDelete(false);

        }
    }
}