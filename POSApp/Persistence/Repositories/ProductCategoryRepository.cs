using System;
using System.Collections.Generic;
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

        public IEnumerable<ProductCategory> GetProductCategories(int storeId)
        {
            return _context.ProductCategories.Where(a=>a.StoreId==storeId).ToList();
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
        public IEnumerable<ProductCategory> GetApiProductCategories()
        {
            IEnumerable<ProductCategory> productCategories = _context.ProductCategories.Where(a => !a.Synced).ToList();
            foreach (var productCategory in productCategories)
            {
                productCategory.Synced = true;
                productCategory.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return productCategories;
        }
    }
}