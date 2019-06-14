using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class TillOperationEntityConfiguration:EntityTypeConfiguration<TillOperation>
    {
        public TillOperationEntityConfiguration()
        {
            ToTable("TillOperations", PosDbContext.DEFAULT_SCHEMA);
            HasKey(a => new {a.Id, a.StoreId});
            Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(a => a.OperationDate).HasColumnType("datetime").IsRequired();
            Property(a => a.Remarks).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(a => a.TillOperationType).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(a => a.Status).HasColumnType("bit").IsOptional();
            Property(a => a.OpeningAmount).HasColumnType("decimal").IsRequired();
            Property(a => a.SystemAmount).HasColumnType("decimal").IsRequired();
            Property(a => a.PhysicalAmount).HasColumnType("decimal").IsRequired();
            Property(x => x.SessionCode).HasColumnType("int").IsRequired();
            Property(x => x.CarryOut).HasColumnType("decimal").IsRequired();
            Property(x => x.AdjustedCashAmount).HasColumnType("decimal").IsRequired();
            Property(x => x.AdjustedCreditAmount).HasColumnType("decimal").IsRequired();
            Property(x => x.AdjustedCreditNoteAmount).HasColumnType("decimal").IsRequired();



            HasOptional(a => a.Shift).WithMany(a => a.TillOperations).HasForeignKey(a => new {a.ShiftId, a.StoreId})
                .WillCascadeOnDelete(false);
            HasRequired(a=>a.Cashier).WithMany(a=>a.TillOperations).HasForeignKey(a=> new{a.ApplicationUserId}).WillCascadeOnDelete(false);
        }
    }
}