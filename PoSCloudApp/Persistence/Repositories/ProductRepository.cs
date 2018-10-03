using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using PoSCloud.Persistence;
using PoSCloudApp.Core.Models;
using EntityState = System.Data.Entity.EntityState;

namespace PoSCloudApp.Persistence.Repositories
{
    public class ProductRepository
    {
        private PosDbContext _context;

        public ProductRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return _context.Products.Where(a => a.CategoryId == productCategoryId).ToList();
        }
        public Product GetProductsById(int id)
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

        public void DeleteProducts(int id)
        {
            var product = new Product { Id = id };
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
        }
    }
}