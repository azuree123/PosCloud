using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using EntityState = System.Data.Entity.EntityState;

namespace POSApp.Persistence.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private PosDbContext _context;

        public ProductRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts(int storeId)
        {
            return _context.Products.Include(a=>a.ProductCategory).Where(a=>a.StoreId==storeId).ToList();
        }
        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return _context.Products.Where(a => a.CategoryId == productCategoryId).ToList();
        }
        public Product GetProductById(int id, int storeid)
        {
            return _context.Products.Include(a=>a.ComboProducts).FirstOrDefault(a=>a.Id==id&&a.StoreId==storeid);
        }
        public Product GetProductByCode(string id, int storeid)
        {
            return _context.Products.Include(a => a.ComboProducts).FirstOrDefault(a => a.ProductCode == id && a.StoreId == storeid);
        }
        public void AddProduct(Product product)
        {
            if (!_context.Products.Where(a => a.Name == product.Name && a.CategoryId==product.CategoryId && a.Type == product.Type && a.StoreId == product.StoreId).Any())
            {
            _context.Products.Add(product);
            }
        }

        public void UpdateProduct(int id, int storeid,Product product)
        {
            product.StoreId = storeid;
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int id, int storeid)
        {
            var product = new Product { Id = id, StoreId = storeid};
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
        }
        public IEnumerable<Product> GetApiProducts()
        {
            IEnumerable<Product> products = _context.Products.Where(a => !a.Synced).ToList();
            foreach (var product in products)
            {
                product.Synced = true;
                product.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return products;
        }
    }
}