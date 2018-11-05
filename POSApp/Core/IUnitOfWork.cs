using POSApp.Core.Repositories;

namespace POSApp.Core
{
    public interface IUnitOfWork
    {
        IStateRepository StateRepository { get; }
        ICityRepository CityRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        //IDesignationRepository DesignationRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ILocationRepository LocationRepository { get; }
        ISaleOrderRepository SaleOrderRepository { get; }
        ISaleOrderDetailRepository SaleOrderDetailRepository { get; }
        IPurchaseOrderRepository PurchaseOrderRepository { get; }
        IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; }
        IExpenseRepository ExpenseRepository { get; }
        IExpenseHeadRepository ExpenseHeadRepository { get; }
        IStoreRepository StoreRepository { get; }
        ICouponRepository CouponRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        ITaxRepository TaxRepository { get; }
        IProductCategoryGroupRepository ProductCategoryGroupRepository { get; }
        ITransDetailRepository TransDetailRepository { get; }
        ITransMasterRepository TransMasterRepository { get; }
        IBusinessPartnerRepository BusinessPartnerRepository { get; }
        IReportsLogRepository ReportsLogRepository { get; }
        IAppCountersRepository AppCountersRepository { get; }
        IClientRepository ClientRepository { get; }
        IUnitRepository UnitRepository { get; }
        IModifierRepository ModifierRepository { get; }
        IModifierOptionRepository ModifierOptionRepository { get; }
        ITimedEventRepository TimedEventRepository { get; }
        IProductsSubRepository ProductsSubRepository { get; }
        ITimedEventProductsRepository TimedEventProductsRepository { get; }
        IFloorRepository FloorRepository { get; }
        void Complete();
    }
}
