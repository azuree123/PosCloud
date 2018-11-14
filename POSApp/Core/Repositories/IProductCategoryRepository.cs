﻿using System.Collections.Generic;
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
    }
}
