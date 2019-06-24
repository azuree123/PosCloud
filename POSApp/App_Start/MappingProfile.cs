using System;
using System.Linq;
using AutoMapper;
using POSApp.Core.Dtos;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Models;
using POSApp.Services;

namespace POSApp
{
    class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateViewModel,Product>();
            CreateMap<Product, ProductCreateViewModel>().ForMember(a => a.Modifiers, o => o.MapFrom(g=>g.ModifierLinkProducts.Select(a=>a.ModifierId).ToArray()));
            CreateMap<ProductDtViewModel, Product>();
            CreateMap<Product, ProductDtViewModel>();
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
            CreateMap<Product, ProductSyncViewModel>();
            CreateMap<TransMaster, TransMasterViewModel>().ForMember(d => d.TransDate, o => o.MapFrom(g => g.TransDate.ToString("yyyy-MMM-dd ddd")))
                .ForMember(d => d.TransTime, o => o.MapFrom(g => g.TransDate.ToShortTimeString()))
                .ForMember(d => d.PaymentMethod, o => o.MapFrom(g =>string.Join(",", g.TransMasterPaymentMethods.Select(a => a.Method + " (" + a.Amount + ")")) ))
                .ForMember(d => d.BusinessPartnerName, o => o.MapFrom(g => g.BusinessPartner.Name))
                .ForMember(k => k.StoreName, j => j.MapFrom(l => l.Store.Name)).ForMember(v=>v.WarehouseName, n=>n.MapFrom(m=>m.Warehouse.Name))
                .ForMember(c=>c.WarehouseName, z=>z.MapFrom(l=>l.Warehouse.Name));
            CreateMap<TransDetail, TransDetailViewModel>()
                .ForMember(d => d.Modifiers,
                    o => o.MapFrom(g => string.Join(",",
                        g.ModifierTransDetail.Select(a =>
                            a.ModifierOption.Name + " (" + a.UnitPrice + ") x " + a.Quantity))))
                .ForMember(d => d.ModifiersPrice,
                    o => o.MapFrom(g => g.ModifierTransDetail.Sum(a => a.UnitPrice * a.Quantity)))
                .ForMember(d => d.ProductName, o => o.MapFrom(g => g.Product.Name))
                .ForMember(f => f.UnitName, h => h.MapFrom(i => i.Product.PurchaseUnit));
                
                
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
            CreateMap<PurchasingDdlViewModel, TransMaster>();
            CreateMap<TransMaster, PurchasingDdlViewModel>();
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

            CreateMap<ShiftViewModel, Shift>();
            CreateMap<Shift, ShiftViewModel>();

            CreateMap<TillOperationViewModel, TillOperation>();
            CreateMap<TillOperation, TillOperationViewModel>();



            CreateMap<RegisterListViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterListViewModel>().ForMember(a=>a.Email,o=>o.MapFrom(g=>g.Email))
                .ForMember(a => a.UserId, o => o.MapFrom(g => g.Id)).ForMember(a => a.UserName, o => o.MapFrom(g => g.UserName))
                .ForMember(a => a.UserPassword, o => o.MapFrom(g => Security.DecryptString(g.PasswordEncrypt,g.Email)));
            CreateMap<TransMasterPaymentMethodViewModel, TransMasterPaymentMethod>();
            CreateMap<TransMasterPaymentMethod, TransMasterPaymentMethodViewModel>();


            CreateMap<ProductsSub, ProductSubViewModel>();
            CreateMap<ProductSubViewModel, ProductsSub>();
            CreateMap<ModifierLinkProduct, ModifierLinkProductViewModel>();
            CreateMap<ModifierLinkProductViewModel, ModifierLinkProduct>();
            CreateMap<IncrementalSyncronizationViewModel, IncrementalSyncronization>();
            CreateMap<IncrementalSyncronization, IncrementalSyncronizationViewModel>();



            CreateMap<Modifier, ModifierListViewModel>().ForMember(a => a.LinkedProducts,
                    g => g.MapFrom(o => o.ModifierLinkProducts.Count))
                .ForMember(a => a.ModifierOptions, g => g.MapFrom(o => o.ModifierOptions.Count));
            CreateMap<ModifierTransDetail, ModifierTransDetailViewModel>();
            CreateMap<ModifierTransDetailViewModel, ModifierTransDetail>();

            CreateMap<Size, SizeViewModel>();
            CreateMap<SizeViewModel, Size>();

            CreateMap<Recipe, RecipeViewModel>();
            CreateMap<RecipeViewModel, Recipe>();

            CreateMap<Warehouse, WarehouseViewModel>();
            CreateMap<WarehouseViewModel, Warehouse>();

            CreateMap<Product, ItemsViewModel>();
            CreateMap<ItemsViewModel, Product>();

            CreateMap<Product, PosProducts>()
                .ForMember(a=>a.ProductImage,g=>g.MapFrom(o => ImageReturn(Convert.ToBase64String(o.Image))))
                .ForMember(a => a.ProductCode, g => g.MapFrom(o => o.ProductCode))
                .ForMember(a => a.ProductId, g => g.MapFrom(o => o.Id))
                .ForMember(a => a.ProductName, g => g.MapFrom(o => o.Name))
                .ForMember(a => a.StoreId, g => g.MapFrom(o => o.StoreId))
                .ForMember(a => a.Size, g => g.MapFrom(o => o.Size));

            CreateMap<ProductCategory, PosCategory>()
                .ForMember(a => a.CategoryImage, g => g.MapFrom(o => ImageReturn(Convert.ToBase64String(o.Image))))
                .ForMember(a => a.CategoryGroup, g => g.MapFrom(o => o.Type))
                .ForMember(a => a.CategoryId, g => g.MapFrom(o => o.Id))
                .ForMember(a => a.CategoryName, g => g.MapFrom(o => o.Name))
                .ForMember(a => a.StoreId, g => g.MapFrom(o => o.StoreId));

            CreateMap<ComboProductsTransDetail, ComboProductsTransDetailViewModel>();
            CreateMap<ComboProductsTransDetailViewModel, ComboProductsTransDetail>();
        }
        private string ImageReturn(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return "/Pos/notfound_placeholder.svg";
            }
            else
            {
                return string.Format("data:image/jpg;base64,{0}", image);
            }
        }

    }
}
