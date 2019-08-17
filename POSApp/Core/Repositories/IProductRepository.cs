using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
   public interface IProductRepository
   {
       IEnumerable<Product> GetAllProducts(int storeId);
       IEnumerable<Product> GetProducts(int productCategoryId);
       Product GetProductById(int id, int storeid);
       void AddProduct(Product product);
       void UpdateProduct(string id, int storeid,Product product);
       void DeleteProduct(string id,int storeid);
       IEnumerable<Product> GetApiProducts();
       Product GetProductByCode(string id, int storeid);
       IQueryable<ProductDtViewModel> GetProductsQuery(int storeId);
       Task<IEnumerable<Product>> GetAllProductsAsync(int storeId);
       Task<Product> GetProductByIdAsync(int id, int storeid);
       Task AddProductAsync(Product product);
       IEnumerable<Product> GetSaleProducts(int productCategoryId, int storeId);
       IEnumerable<Product> GetSaleProductsQuery(int storeId,string term);
       Task<IEnumerable<Product>> GetAllProductsAsyncIncremental(int storeId, DateTime date);
       IEnumerable<Product> GetProductsNotInventory(int storeId);
       IEnumerable<Product> GetInventoryProducts(int storeId);
   }
}
