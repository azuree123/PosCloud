using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IProductRepository
   {
       IEnumerable<Product> GetAllProducts();
       IEnumerable<Product> GetProducts(int productCategoryId);
       Product GetProductById(int id);
       void AddProduct(Product product);
       void UpdateProduct(int id, Product product);
       void DeleteProduct(int id);
   }
}
