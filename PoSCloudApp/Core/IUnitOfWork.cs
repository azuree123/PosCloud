using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoSCloudApp.Core.Repositories;

namespace PoSCloudApp.Core
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

        void Complete();
    }
}
