using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoSCloudApp.Core;
using PoSCloudApp.Core.Repositories;
using PoSCloudApp.Persistence.Repositories;

namespace PoSCloudApp.Persistence
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
        public IDesignationRepository DesignationRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ILocationRepository LocationRepository { get; private set; }
        public ISaleOrderRepository SaleOrderRepository { get; private set; }
        public ISaleOrderDetailRepository SaleOrderDetailRepository { get; private set; }
        public IPurchaseOrderRepository PurchaseOrderRepository { get; private set; }
        public IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; private set; }
        public IExpenseRepository ExpenseRepository { get; private set; }
        public IExpenseHeadRepository ExpenseHeadRepository { get; private set; }
        public UnitOfWork(PosDbContext context)
        {
            _context = context;
            StateRepository=new StateRepository(context);
            CityRepository = new CityRepository(context);
            ProductRepository = new ProductRepository(context);
            ProductCategoryRepository = new ProductCategoryRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            DesignationRepository = new DesignationRepository(context);
            CustomerRepository = new CustomerRepository(context);
            SupplierRepository = new SupplierRepository(context);
            LocationRepository = new LocationRepository(context);
            SaleOrderRepository = new SaleOrderRepository(context);
            SaleOrderDetailRepository = new SaleOrderDetailRepository(context);
            PurchaseOrderRepository = new PurchaseOrderRepository(context);
            PurchaseOrderDetailRepository = new PurchaseOrderDetailRepository(context);
            ExpenseRepository = new ExpenseRepository(context);
            ExpenseHeadRepository = new ExpenseHeadRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();

        }
    }
}
