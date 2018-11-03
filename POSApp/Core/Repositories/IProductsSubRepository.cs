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
       ProductsSub GetProductsSubById(int id, int comboProductId, int storeid);
       void AddProductsSub(ProductsSub productsSubs);
       void UpdateProductsSub(int id, int comboProductId, ProductsSub productsSub, int storeid);
       void DeleteProductsSub(int id, int comboProductId, int storeid);
       IEnumerable<ProductsSub> GetApiProductsSubs();

   }
}
