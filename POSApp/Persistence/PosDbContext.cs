using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using POSApp.Core.Models;
using POSApp.Models;
using POSApp.Persistence.EntityConfigurations;

using RedirectToRouteResult = System.Web.Http.Results.RedirectToRouteResult;

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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseHead> ExpenseHeads { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<TransDetail> TransDetails { get; set; }
        public DbSet<TransMaster> TransMasters { get; set; }
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<ProductCategoryGroup> ProductCategoryGroups { get; set; }
        public DbSet<ReportsLog> ReportsLogs { get; set; }
        public DbSet<AppCounter> AppCounters { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<ModifierOption> ModifierOptions { get; set; }
        public DbSet<TimedEvent> TimedEvents { get; set; }
        public DbSet<ProductsSub> ProductsSubs { get; set; }
        public DbSet<TimedEventProducts> TimedEventProducts { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<DineTable> DineTables { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SecurityRight> SecurityRights { get; set; }
        public DbSet<SecurityObject> SecurityObjects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<POSTerminal> PosTerminals { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TillOperation> TillOperations { get; set; }
        public DbSet<TransMasterPaymentMethod> TransMasterPaymentMethods { get; set; }
        public DbSet<ModifierLinkProduct> ModifierLinkProducts { get; set; }
        public DbSet<ModifierTransDetail> ModifierTransDetails { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<IncrementalSyncronization>  IncrementalSyncronizations { get; set; }
        public DbSet<ComboProductsTransDetail> ComboProductsTransDetails { get; set; }

        public DbSet<UserStore> UserStores { get; set; }
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
            modelBuilder.Configurations.Add(new DesignationEntityConfiguration());
            //modelBuilder.Configurations.Add(new UserLoginConfiguration());
            //modelBuilder.Configurations.Add(new UserClaimConfiguration());

            modelBuilder.Configurations.Add(new StateEntityConfiguration());
            modelBuilder.Configurations.Add(new CityEntityConfiguration());
            modelBuilder.Configurations.Add(new DepartmentEntityConfiguration());
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new ExpenseEntityConfiguration());
            modelBuilder.Configurations.Add(new ExpenseHeadEntityConfiguration());
            modelBuilder.Configurations.Add(new LocationEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new StoreEntityConfiguration());
            //modelBuilder.Configurations.Add(new CouponEntityConfiguration());
            modelBuilder.Configurations.Add(new TaxEntityConfiguration());
            modelBuilder.Configurations.Add(new DiscountEntityConfiguration());
            modelBuilder.Configurations.Add(new TransDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new TransMasterEntityConfiguration());
            modelBuilder.Configurations.Add(new BusinessPartnerEntityConfiguration());
            modelBuilder.Configurations.Add(new UnitEntityConfiguration());
            modelBuilder.Configurations.Add(new TimedEventEntityConfiguration());
            modelBuilder.Configurations.Add(new ModifierEntityConfiguration());
            modelBuilder.Configurations.Add(new ModifierOptionEntityConfiguration());
            modelBuilder.Configurations.Add(new ReportLogEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductsSubEntityConfiguration());
            modelBuilder.Configurations.Add(new TimedEventProductEntityConfiguration());
            modelBuilder.Configurations.Add(new DineTableEntityConfiguration());
            modelBuilder.Configurations.Add(new FloorEntityConfiguration());
            modelBuilder.Configurations.Add(new DeviceEntityConfiguration());
            modelBuilder.Configurations.Add(new SecurityRightEntityConfiguration());
            modelBuilder.Configurations.Add(new SectionEntityConfiguration());
            modelBuilder.Configurations.Add(new POSTerminalEntityConfiguration());
            modelBuilder.Configurations.Add(new ShiftEntityConfiguration());
            modelBuilder.Configurations.Add(new TillOperationEntityConfiguration());
            modelBuilder.Configurations.Add(new TransMasterPaymentMethodEntityConfiguration());
            modelBuilder.Configurations.Add(new ModifierLinkProductEntityConfiguration());
            modelBuilder.Configurations.Add(new ModifierTransDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new SizeEntityConfiguration());
            modelBuilder.Configurations.Add(new RecipeEntityConfiguration());
            modelBuilder.Configurations.Add(new WarehouseEntityConfiguration());
            modelBuilder.Configurations.Add(new ClientEntityConfiguration());
            modelBuilder.Configurations.Add(new IncrementalSyncronizationEntityConfiguration());
            modelBuilder.Configurations.Add(new UserStoreEntityConfiguration());
            modelBuilder.Configurations.Add(new AppCounterEntityConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserEntityConfiguration());
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
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedOn = DateTime.Now;
                        entity.CreatedById = HttpContext.Current.User.Identity.GetUserId();

                    }

                    entity.UpdatedById = HttpContext.Current.User.Identity.GetUserId();
                    entity.UpdatedOn = DateTime.Now;
                }
               
            }

            return base.SaveChanges();
        }

    }
}
