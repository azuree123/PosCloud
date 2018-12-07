using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class TransMasterPaymentMethodEntityConfiguration:EntityTypeConfiguration<TransMasterPaymentMethod>
    {
        public TransMasterPaymentMethodEntityConfiguration()
        {
            ToTable("TransMasterPaymentMethods", PosDbContext.DEFAULT_SCHEMA);

            HasKey(a => a.Id);
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Method).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Amount).HasColumnType("float").IsRequired();
            HasRequired(x=>x.TransMaster).WithMany(x=>x.TransMasterPaymentMethods).HasForeignKey(x=>x.TransMasterId).WillCascadeOnDelete(false);
        }
    }
}