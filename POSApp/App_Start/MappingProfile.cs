﻿using System;
using System.Linq;
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
            //CreateMap<CouponModelView, Coupon>();
            //CreateMap<Coupon, CouponModelView>();
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
            CreateMap<TimedEventViewModel, TimedEvent>().ForMember(a => a.Days, o => o.MapFrom(g => string.Join(",",g.Days)));
            CreateMap<TimedEvent, TimedEventViewModel>().ForMember(a=>a.BranchesDisplay,o=>o.MapFrom(g=>g.StoreId))
                .ForMember(a => a.ProductsDisplay, o => o.MapFrom(g => string.Join(",",g.TimedEventProducts.Select(a=>a.ProductCode).ToArray())))
                .ForMember(a => a.DaysDisplay, o => o.MapFrom(g => g.Days))
                .ForMember(a => a.Days, o => o.MapFrom(g => g.Days.Split(',')))
                .ForMember(a => a.Products, o => o.MapFrom(g => g.TimedEventProducts.Select(a => a.ProductCode).ToArray()));
            CreateMap<DineTableViewModel, DineTable>();
            CreateMap<DineTable, DineTableViewModel>();
            CreateMap<FloorViewModel, Floor>();
            CreateMap<Floor, FloorViewModel>();

            CreateMap<ComboViewModel, Product>();
            CreateMap<Product, ComboViewModel>().ForMember(a => a.ProductSubViewModels,o=>o.MapFrom(g=>g.ComboProducts));
            CreateMap<DeviceViewModel, Device>();
            CreateMap<Device, DeviceViewModel>();

            CreateMap<ProductCategory, ProductCategoryDdlViewModel>();
            CreateMap<ProductDdlViewModel, Product>();
            CreateMap<Product, ProductDdlViewModel>();
            CreateMap<UserViewModel, ApplicationUser>().ForMember(d => d.UserName, o => o.MapFrom(g => g.Name));
            CreateMap<ApplicationUser, UserViewModel>().ForMember(d => d.Name, o => o.MapFrom(g => g.UserName));

            CreateMap<SecurityObjectViewModel, SecurityObject>();
            CreateMap<SecurityObject, SecurityObjectViewModel>();

            CreateMap<SecurityRightViewModel, SecurityRight>();
            CreateMap<SecurityRight, SecurityRightViewModel>();

            CreateMap<SectionViewModel, Section>();
            CreateMap<Section, SectionViewModel>();

            CreateMap<POSTerminalViewModel, POSTerminal>();
            CreateMap<POSTerminal, POSTerminalViewModel>();

            CreateMap<POSTerminal, POSTerminalListModelView>().ForMember(a=>a.Section,o=>o.MapFrom(g=>g.Section.Name))
                .ForMember(a => a.POSTerminalId, o => o.MapFrom(g => g.POSTerminalId))
                .ForMember(a => a.POSTerminalName, o => o.MapFrom(g => g.Name));

        }
    }
}
