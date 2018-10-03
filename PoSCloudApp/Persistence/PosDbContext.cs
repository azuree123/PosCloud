using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PoSCloud.Core.Models;
using PoSCloudApp.Core.Models.DbModels;
using PoSCloudApp.Persistence.EntityConfigurations;

namespace PoSCloud.Persistence
{
    public class PosDbContext : IdentityDbContext<ApplicationUser>
    {
        public const string DEFAULT_SCHEMA = "PosCloud";
        public PosDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.SetCommandTimeOut(300);
        }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }
        public static PosDbContext Create()
        {

            return new PosDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StateEntityConfiguration());
            modelBuilder.Configurations.Add(new CityEntityConfiguration());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

        }
        public override int SaveChanges()
        {
            // get added or updated entries ///    //"Tech@381"
            var addedOrUpdatedEntries = ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            // fill out the audit fields
            foreach (var entry in addedOrUpdatedEntries)
            {
                var entity = entry.Entity as AuditableEntity;

                if (entry.State == EntityState.Added)
                {
                    //entity.CreatedBy = _userInfoService.UserId;
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedBy = "Waqar";

                }

                entity.UpdatedBy = "Waqar";
                entity.UpdatedOn = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

    }
}
