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
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(a=>a.ProductCategory).ToList();
        }
        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return _context.Products.Where(a => a.CategoryId == productCategoryId).ToList();
        }
        public Product GetProductById(int id, int storeid)
        {
            return _context.Products.Find(id,storeid);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
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
    }
}