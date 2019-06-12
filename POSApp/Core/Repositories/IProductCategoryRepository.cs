using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetProductCategories(int storeId);
        ProductCategory GetProductCategoryById(int id, int storeid);
        void AddProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(int id,int storeid ,ProductCategory productCategory);
        void DeleteProductCategory(int id,int storeid);
        IEnumerable<ProductCategory> GetApiProductCategories();
        ProductCategory GetProductCategoryByCode(string code, int storeid);
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync(int storeId);
        Task<ProductCategory> GetProductCategoryByIdAsync(int id, int storeid);
        Task AddProductCategoryAsync(ProductCategory productCategory);
        Task<IEnumerable<ProductCategory>> GetAllProductCategoryAsyncIncremental(int storeId, DateTime date);
    }
}
