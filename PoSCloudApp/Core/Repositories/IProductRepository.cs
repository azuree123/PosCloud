using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoSCloudApp.Core.Models;

namespace PoSCloudApp.Core.Repositories
{
   public interface IProductRepository
   {
       IEnumerable<Product> GetProducts(int productCategoryId);
       Product GetProductById(int id);
       void AddProduct(Product product);
       void UpdateProduct(int id, Product product);
       void DeleteProduct(int id);
   }
}
