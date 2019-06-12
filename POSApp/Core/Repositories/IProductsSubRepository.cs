using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IProductsSubRepository
   {
       IEnumerable<ProductsSub> GetProductsSubs(int storeid);
       ProductsSub GetProductsSubById(string id, string comboProductId, int storeid);
       void AddProductsSub(ProductsSub productsSubs);
       void UpdateProductsSub(string id, string comboProductId, ProductsSub productsSub, int storeid);
       void DeleteProductsSub(string id, string comboProductId, int storeid);
       IEnumerable<ProductsSub> GetApiProductsSubs();
       IEnumerable<ProductsSub> GetProductsSubs(string productCode, int storeid);
       Task<IEnumerable<ProductsSub>> GetProductsSubsAsync(int storeid);
       Task<ProductsSub> GetProductsSubByIdAsync(string id, string comboProductId, int storeid);
       Task AddProductsSubAsync(ProductsSub productsSubs);
       Task<IEnumerable<ProductsSub>> GetAllProductSubsAsyncIncremental(int storeId, DateTime date);

   }
}
