using PoSCloudApp.Core.Models;
using AutoMapper;
using PoSCloudApp.Core.ViewModels;

namespace PoSCloudApp
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
            CreateMap<DesignationViewModel, Designation>();
            CreateMap<Designation, DesignationViewModel>();


        }
    }
}
