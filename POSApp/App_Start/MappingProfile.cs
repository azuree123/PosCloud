using System;
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
            CreateMap<UnitViewModel, Unit>();
            CreateMap<Unit, UnitViewModel>();
            CreateMap<ClientViewModel, Client>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<ProductSyncViewModel, Product>() ;
            CreateMap<Product, ProductSyncViewModel>().ForMember(d => d.Image, o => o.MapFrom(g => Convert.ToBase64String(g.Image)));
            CreateMap<TransMaster, TransMasterViewModel>().ForMember(d => d.TransDate, o => o.MapFrom(g => g.TransDate.ToString("yyyy-MMM-dd ddd")))
                .ForMember(d => d.TransTime, o => o.MapFrom(g => g.TransDate.ToShortTimeString()))
                .ForMember(d => d.BusinessPartnerName, o => o.MapFrom(g => g.BusinessPartner.Name));
            CreateMap<TransDetail, TransDetailViewModel>()
                .ForMember(d => d.ProductName, o => o.MapFrom(g => g.Product.Name));
            CreateMap<TransMasterViewModel, TransMaster>();
            CreateMap<TransDetailViewModel, TransDetail>();
            CreateMap<ModifierViewModel, Modifier>();
            CreateMap<Modifier, ModifierViewModel>().ForMember(a => a.ModifierOptionViewModels, o => o.MapFrom(a=>a.ModifierOptions));
            CreateMap<ModifierOptionViewModel, ModifierOption>();
            CreateMap<ModifierOption, ModifierOptionViewModel>();
        }
    }
}
