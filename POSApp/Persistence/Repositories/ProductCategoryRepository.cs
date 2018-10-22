﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        private PosDbContext _context;

        public ProductCategoryRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _context.ProductCategories.ToList();
        }
        public ProductCategory GetProductCategoryById(int id, int storeid)
        {
            return _context.ProductCategories.Find(id,storeid);
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }

        public void UpdateProductCategory(int id,int storeid ,ProductCategory productCategory)
        {
            productCategory.StoreId = storeid;
            _context.ProductCategories.Attach(productCategory);
            _context.Entry(productCategory).State = EntityState.Modified;
        }

        public void DeleteProductCategory(int id,int storeid)
        {
            var productCategory = new ProductCategory { Id = id, StoreId = storeid};
            _context.ProductCategories.Attach(productCategory);
            _context.Entry(productCategory).State = EntityState.Deleted;
        }
    }
}