using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PoSCloudApp.Persistence;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.Repositories;
using EntityState = System.Data.Entity.EntityState;

namespace PoSCloudApp.Persistence.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private PosDbContext _context;

        public ProductRepository(PosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(a=>a.ProductCategory).ToList();
        }
        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return _context.Products.Where(a => a.CategoryId == productCategoryId).ToList();
        }
        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(int id, Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int id)
        {
            var product = new Product { Id = id };
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
        }
    }
}