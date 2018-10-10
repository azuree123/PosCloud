using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Ninject.Activation;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Persistence.EntityConfigurations;
using PoSCloudApp.Persistence.Repositories;
using PoSCloudApp.Services;

namespace PoSCloudApp.Persistence
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseHead> ExpenseHeads { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public DbSet<Designation> Designations { get; set; }

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
            modelBuilder.Configurations.Add(new CustomerEntityConfiguration());
            modelBuilder.Configurations.Add(new DepartmentEntityConfiguration());
            modelBuilder.Configurations.Add(new DesignationEntityConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new ExpenseEntityConfiguration());
            modelBuilder.Configurations.Add(new ExpenseHeadEntityConfiguration());
            modelBuilder.Configurations.Add(new LocationEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new PurchaseOrderDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new PurchaseOrderEntityConfiguration());
            modelBuilder.Configurations.Add(new SaleOrderDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new SaleOrderEntityConfiguration());
            modelBuilder.Configurations.Add(new SupplierEntityConfiguration());
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
                    entity.CreatedOn = DateTime.Now;
                    entity.CreatedBy = HttpContext.Current.User.Identity.Name;

                }

                entity.UpdatedBy = HttpContext.Current.User.Identity.GetUserName();
                entity.UpdatedOn = DateTime.Now;
            }

            return base.SaveChanges();
        }

    }
}
