using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using POSApp.Core.Models;

namespace POSApp.Persistence.EntityConfigurations
{
    public class CustomerEntityConfiguration : EntityTypeConfiguration<Customer>
    {

        public CustomerEntityConfiguration()
        {
            ToTable("Customers", PosDbContext.DEFAULT_SCHEMA);
            //******************************************************************************************* KEYS ********************
            HasKey(x => new { x.Id, x.StoreId });
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //******************************************************************************************* PROPERTIES ***************
            Property(x => x.Name).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            Property(x => x.PhoneNumber).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Address).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Birthday).HasColumnType("datetime").IsOptional();
            Property(x => x.City).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.State).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(150).IsOptional();
           // Property(x => x.Gender).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Note).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            //Property(x => x.Referral).HasColumnType("nvarchar").HasMaxLength(150).IsOptional();
            Property(x => x.Code).HasColumnType("varchar").HasMaxLength(150).IsOptional();



            //HasOptional(x => x.CreatedBy).WithMany().HasForeignKey(x =>  new {x.CreatedById,x.StoreId}).WillCascadeOnDelete(false);
            //HasOptional(x => x.UpdatedBy).WithMany().HasForeignKey(x => new { x.UpdatedById, x.StoreId }).WillCascadeOnDelete(false);


        }

    }
}