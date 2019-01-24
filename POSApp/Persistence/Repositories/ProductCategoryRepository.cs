using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
            return _context.ProductCategories.Where(a=>a.StoreId==storeId && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync(int storeId)
        {
            return await _context.ProductCategories.Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public ProductCategory GetProductCategoryById(int id, int storeid)
        {
            return _context.ProductCategories.Find(id,storeid);
        }
        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id, int storeid)
        {
            return await _context.ProductCategories.FindAsync(id, storeid);
        }
        public ProductCategory GetProductCategoryByCode(string code, int storeid)
        {
            return _context.ProductCategories.FirstOrDefault(a=>a.Code==code && a.StoreId==storeid);
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            var inDb = _context.ProductCategories.FirstOrDefault(a =>
                a.Name == productCategory.Name && a.Type == productCategory.Type &&
                a.StoreId == productCategory.StoreId);
            if (inDb == null)
            {
                _context.ProductCategories.Add(productCategory);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    productCategory.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(productCategory);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            var inDb = await _context.ProductCategories.FirstOrDefaultAsync(a =>
                a.Name == productCategory.Name && a.Type == productCategory.Type &&
                a.StoreId == productCategory.StoreId);
            if (inDb == null)
            {
                _context.ProductCategories.Add(productCategory);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    productCategory.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(productCategory);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateProductCategory(int id,int storeid ,ProductCategory productCategory)
        {
            productCategory.StoreId = storeid;
            _context.ProductCategories.Attach(productCategory);
            _context.Entry(productCategory).State = EntityState.Modified;
        }

        public void DeleteProductCategory(int id,int storeid)
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(a => a.Id == id && a.StoreId == storeid);
            productCategory.IsDisabled = true;
            _context.ProductCategories.Attach(productCategory);
            _context.Entry(productCategory).State = EntityState.Modified;
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