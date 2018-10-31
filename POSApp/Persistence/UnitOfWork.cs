using POSApp.Core;
using POSApp.Core.Repositories;
using POSApp.Persistence.Repositories;

namespace POSApp.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly PosDbContext _context;

        public IStateRepository StateRepository { get;private set; }
        public ICityRepository CityRepository { get;private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IProductCategoryRepository ProductCategoryRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        //public IDesignationRepository DesignationRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ILocationRepository LocationRepository { get; private set; }
        public ISaleOrderRepository SaleOrderRepository { get; private set; }
        public ISaleOrderDetailRepository SaleOrderDetailRepository { get; private set; }
        public IPurchaseOrderRepository PurchaseOrderRepository { get; private set; }
        public IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; private set; }
        public IExpenseRepository ExpenseRepository { get; private set; }
        public IExpenseHeadRepository ExpenseHeadRepository { get; private set; }
        public IStoreRepository StoreRepository { get; private set; }
        public ICouponRepository CouponRepository { get; private set; }
        public ITaxRepository TaxRepository { get; private set; }
        public IDiscountRepository DiscountRepository { get; private set; }

        public IProductCategoryGroupRepository ProductCategoryGroupRepository { get; private set; }

        public ITransDetailRepository TransDetailRepository { get; private set; }
        public ITransMasterRepository TransMasterRepository { get; private set; }
        public IBusinessPartnerRepository BusinessPartnerRepository { get; private set; }
        public IReportsLogRepository ReportsLogRepository { get; private set; }

       public IAppCountersRepository AppCountersRepository { get; private set; }
        public IUnitRepository UnitRepository { get; private set; }

        public UnitOfWork(PosDbContext context)
        {
            _context = context;
            StateRepository=new StateRepository(context);
            CityRepository = new CityRepository(context);
            ProductRepository = new ProductRepository(context);
            ProductCategoryRepository = new ProductCategoryRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            //DesignationRepository = new DesignationRepository(context);
            CustomerRepository = new CustomerRepository(context);
            SupplierRepository = new SupplierRepository(context);
            LocationRepository = new LocationRepository(context);
            SaleOrderRepository = new SaleOrderRepository(context);
            SaleOrderDetailRepository = new SaleOrderDetailRepository(context);
            PurchaseOrderRepository = new PurchaseOrderRepository(context);
            PurchaseOrderDetailRepository = new PurchaseOrderDetailRepository(context);
            ExpenseRepository = new ExpenseRepository(context);
            ExpenseHeadRepository = new ExpenseHeadRepository(context);
            StoreRepository = new StoreRepository(context);
            CouponRepository = new CouponRepository(context);
            TaxRepository = new TaxRepository(context);
            DiscountRepository = new DiscountRepository(context);
            ProductCategoryGroupRepository = new ProductCategoryGroupRepository(context);
            BusinessPartnerRepository = new BusinessPartnerRepository(context);
            TransDetailRepository = new TransDetailRepository(context);
            TransMasterRepository = new TransMasterRepository(context);
            ReportsLogRepository = new ReportsLogRepository(context);
            AppCountersRepository=new AppCountersRepository(context);
            UnitRepository = new UnitRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();

        }
    }
}
