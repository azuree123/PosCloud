using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
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
            return _context.Products.Include(a=>a.ProductCategory).Where(a=>a.StoreId==storeId && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync(int storeId)
        {
            return await _context.Products.Include(a => a.ProductCategory).Where(a => a.StoreId == storeId && !a.IsDisabled).ToListAsync();
        }
        public IEnumerable<Product> GetProducts(int productCategoryId)
        {
            return _context.Products.Where(a => a.CategoryId == productCategoryId && !a.IsDisabled).ToList();
        }
        public Product GetProductById(int id, int storeid)
        {
            return _context.Products.Include(a=>a.ComboProducts.Where(o=>!o.IsDisabled)).FirstOrDefault(a=>a.Id==id&&a.StoreId==storeid);
        }
        public async Task<Product> GetProductByIdAsync(int id, int storeid)
        {
            return await _context.Products.Include(a => a.ComboProducts.Where(o => !o.IsDisabled)).FirstOrDefaultAsync(a => a.Id == id && a.StoreId == storeid);
        }
        public Product GetProductByCode(string id, int storeid)
        {
            return _context.Products.Include(a => a.ComboProducts.Where(o => !o.IsDisabled)).FirstOrDefault(a => a.ProductCode == id && a.StoreId == storeid);
        }
        public IQueryable<ProductDtViewModel> GetProductsQuery(int storeId)
        {
            //return _context.PurchaseOrder;
            return Mapper.Map<ProductDtViewModel[]>(_context.Products
                .Where(a => a.StoreId == storeId && !a.IsDisabled)).AsQueryable();

        }
        public void AddProduct(Product product)
        {
            var inDb = _context.Products.FirstOrDefault(a =>
                a.Name == product.Name && a.Size == product.Size && a.CategoryId == product.CategoryId && a.Type == product.Type &&
                a.StoreId == product.StoreId);
            if (inDb == null)
            {
                _context.Products.Add(product);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    product.Id = inDb.Id;
                    product.ProductCode = inDb.ProductCode;
                    _context.Entry(inDb).CurrentValues.SetValues(product);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public async Task AddProductAsync(Product product)
        {
            var inDb = await _context.Products.FirstOrDefaultAsync(a =>
                a.Name == product.Name && a.Size == product.Size && a.CategoryId == product.CategoryId && a.Type == product.Type &&
                a.StoreId == product.StoreId);
            if (inDb == null)
            {
                _context.Products.Add(product);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    product.Id = inDb.Id;
                    product.ProductCode = inDb.ProductCode;
                    _context.Entry(inDb).CurrentValues.SetValues(product);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }
        }
        public void UpdateProduct(string id, int storeid,Product product)
        {
            product.StoreId = storeid;
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(string id, int storeid)
        {
            var product = _context.Products.FirstOrDefault(a=>a.ProductCode==id && a.StoreId==storeid);
            product.IsDisabled = true;
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
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