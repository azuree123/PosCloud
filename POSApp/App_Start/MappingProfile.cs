﻿using AutoMapper;
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
            CreateMap<CustomerModelView, Customer>();
            CreateMap<Customer, CustomerModelView>();
            CreateMap<SupplierModelView, Supplier>();
            CreateMap<Supplier, SupplierModelView>();
            CreateMap<StateModelView, State>();
            CreateMap<State, StateModelView>();
            CreateMap<CityModelView, City>();
            CreateMap<City, CityModelView>();
            CreateMap<LocationModelView, Location>();
            CreateMap<Location, LocationModelView>();
            CreateMap<ServiceCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, ServiceCategoryViewModel>();
            CreateMap<ServiceCreateViewModel, Product>().ForMember(a=>a.ProductCode,o=>o.MapFrom(g=>g.ServiceCode));
            CreateMap<Product, ServiceCreateViewModel>().ForMember(a => a.ServiceCode, o => o.MapFrom(g => g.ProductCode));
            CreateMap<StoreViewModel, Store>();
            CreateMap<Store, StoreViewModel>();
            CreateMap<CouponModelView, Coupon>();
            CreateMap<Coupon, CouponModelView>();
            CreateMap<TaxViewModel, Tax>();
            CreateMap<Tax, TaxViewModel>();
            CreateMap<DiscountViewModel, Discount>();
            CreateMap<Discount, DiscountViewModel>();



        }
    }
}