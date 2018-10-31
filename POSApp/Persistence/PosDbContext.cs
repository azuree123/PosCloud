using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using POSApp.Core.Models;
using POSApp.Persistence.EntityConfigurations;

namespace POSApp.Persistence
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
        //public DbSet<Designation> Designations { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<TransDetail> TransDetails { get; set; }
        public DbSet<TransMaster> TransMasters { get; set; }
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<ProductCategoryGroup> ProductCategoryGroups { get; set; }
        public DbSet<ReportsLog> ReportsLogs { get; set; }
        public DbSet<AppCounter> AppCounters { get; set; }

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
            
            //modelBuilder.Configurations.Add(new ApplicationUserEntityConfiguration());
            //modelBuilder.Configurations.Add(new UserRoleConfiguration());
            //modelBuilder.Configurations.Add(new RoleConfiguration());

            //modelBuilder.Configurations.Add(new UserLoginConfiguration());
            //modelBuilder.Configurations.Add(new UserClaimConfiguration());

            modelBuilder.Configurations.Add(new StateEntityConfiguration());
            modelBuilder.Configurations.Add(new CityEntityConfiguration());
            modelBuilder.Configurations.Add(new CustomerEntityConfiguration());
            modelBuilder.Configurations.Add(new DepartmentEntityConfiguration());
            //modelBuilder.Configurations.Add(new DesignationEntityConfiguration());
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
            modelBuilder.Configurations.Add(new StoreEntityConfiguration());
            modelBuilder.Configurations.Add(new CouponEntityConfiguration());
            modelBuilder.Configurations.Add(new TaxEntityConfiguration());
            modelBuilder.Configurations.Add(new DiscountEntityConfiguration());
            modelBuilder.Configurations.Add(new TransDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new TransMasterEntityConfiguration());
            modelBuilder.Configurations.Add(new BusinessPartnerEntityConfiguration());
            modelBuilder.Configurations.Add(new UnitEntityConfiguration());
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
                    entity.CreatedById = HttpContext.Current.User.Identity.GetUserId();

                }

                entity.UpdatedById = HttpContext.Current.User.Identity.GetUserId();
                entity.UpdatedOn = DateTime.Now;
            }

            return base.SaveChanges();
        }

    }
}
