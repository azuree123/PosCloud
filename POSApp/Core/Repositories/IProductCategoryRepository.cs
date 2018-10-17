using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetProductCategories();
        ProductCategory GetProductCategoryById(int id);
        void AddProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(int id, ProductCategory productCategory);
        void DeleteProductCategory(int id);
    }
}
