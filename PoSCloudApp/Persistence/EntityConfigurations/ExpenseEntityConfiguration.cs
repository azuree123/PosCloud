using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class ExpenseEntityConfiguration:EntityTypeConfiguration<Expense>
    {
        public ExpenseEntityConfiguration()
        {
            ToTable("Expenses", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Amount).HasColumnType("float").IsOptional();
            Property(x => x.Date).HasColumnType("datetime").IsOptional();
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            HasRequired(x=>x.ExpenseHead).WithMany(a=>a.Expenses).HasForeignKey(a=>a.ExpenseHeadId).WillCascadeOnDelete(true);
            HasRequired(x=>x.Employee).WithMany(x=>x.Expenses).HasForeignKey(x=>x.EmployeeId).WillCascadeOnDelete(false);
        }
    }
}