using PoSCloudApp.Core.Models;
using AutoMapper;
using PoSCloudApp.Core.Dtos;

namespace PoSCloudApp
{
    class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateUpdateDto,Product>();
            CreateMap<Product,ProductCreateUpdateDto>();
            CreateMap<ProductCategoryDto, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryDto>();


        }
    }
}
