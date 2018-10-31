using AutoMapper;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp
{
    class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateViewModel,Product>();
            CreateMap<Product, ProductCreateViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<DepartmentViewModel, Department>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<ExpenseHeadViewModel, ExpenseHead>();
            CreateMap<ExpenseHead, ExpenseHeadViewModel>();
            CreateMap<ExpenseViewModel, Expense>();
            CreateMap<Expense, ExpenseViewModel>();
            CreateMap<DesignationViewModel, Designation>();
            CreateMap<Designation, DesignationViewModel>();
            CreateMap<EmployeeModelView, Employee>();
            CreateMap<Employee, EmployeeModelView>();
            CreateMap<CustomerModelView, BusinessPartner>();
            CreateMap<BusinessPartner, CustomerModelView>();
            CreateMap<SupplierModelView, BusinessPartner>();
            CreateMap<BusinessPartner, SupplierModelView>();
            CreateMap<StateModelView, State>();
            CreateMap<State, StateModelView>();
            CreateMap<CityModelView, City>();
            CreateMap<City, CityModelView>();
            CreateMap<LocationModelView, Location>();
            CreateMap<Location, LocationModelView>();
            CreateMap<ServiceCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, ServiceCategoryViewModel>();
            CreateMap<ServiceCreateViewModel, Product>();
            CreateMap<Product, ServiceCreateViewModel>();
            CreateMap<StoreViewModel, Store>();
            CreateMap<Store, StoreViewModel>();
            CreateMap<CouponModelView, Coupon>();
            CreateMap<Coupon, CouponModelView>();
            CreateMap<TaxViewModel, Tax>();
            CreateMap<Tax, TaxViewModel>();
            CreateMap<DiscountViewModel, Discount>();
            CreateMap<Discount, DiscountViewModel>();
            CreateMap<ReportLogViewModel, ReportsLog>();
            CreateMap<ReportsLog, ReportLogViewModel>();
            CreateMap<ProductCategoryGroupViewModel, ProductCategoryGroup>();
            CreateMap<ProductCategoryGroup, ProductCategoryGroupViewModel>();
          


        }
    }
}
