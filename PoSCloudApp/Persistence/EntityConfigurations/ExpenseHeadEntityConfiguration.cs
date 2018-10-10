using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Persistence.EntityConfigurations
{
    public class ExpenseHeadEntityConfiguration:EntityTypeConfiguration<ExpenseHead>
    {
        public ExpenseHeadEntityConfiguration()
        {
            ToTable("ExpenseHeads", PosDbContext.DEFAULT_SCHEMA);
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.Details).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();

            Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            Property(x => x.UpdatedBy).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
        }
    }
}