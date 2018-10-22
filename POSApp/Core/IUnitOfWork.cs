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
        IDesignationRepository DesignationRepository { get; }
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
        void Complete();
    }
}
